﻿using System;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements.Charting;
using ceTe.DynamicPDF.PageElements.Charting.Axes;
using ceTe.DynamicPDF.PageElements.Charting.Series;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Integration_API.Dto;
using Integration_API.Mapper;
using Integration.Partnership.Service;
using Integration.Partnership.Repository;
using Integration.Partnership.Model;
using Integration.Pharmacy.Service;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Model;
using ceTe.DynamicPDF.PageElements;
using System.IO;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
       
        TenderService service = new TenderService(new TenderRepository());
        FilesService filesService = new FilesService(new FilesRepository());
        
        [HttpPost]
        public IActionResult CreateTender(TenderCreationDto dto)
        {
            Tender tender = TenderMapper.TenderCreationDtoToTender(dto,Generator.GenerateTenderId());
            service.SaveTender(tender);
            TenderDto tenderDto = TenderMapper.TenderToTenderDto(tender);
            SendTender(tenderDto);
            return Ok();
        }

        public void SendTender(TenderDto tenderDto)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "TenderChannel", type: ExchangeType.Fanout);
                channel.QueueDeclare(queue: "World Vision Clinic",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);


                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tenderDto));

                channel.BasicPublish(exchange: "TenderChannel",
                                     routingKey: "World Vision Clinic",
                                     basicProperties: null,
                                     body: body);

            }
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetReport(DateTime start, DateTime end)
        {
            GenerateReport(start, end);
            string localPath = "Reports/Tender" + start.Month + "-" + start.Day + "-" + start.Year + "to" + end.Month + "-" + end.Day + "-" + end.Year + ".pdf";
            var memory = new System.IO.MemoryStream();
            using (var stream = new FileStream(localPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var contentType = "APPLICATION/octet-stream";
            var fileName = System.IO.Path.GetFileName(localPath);

            return File(memory, contentType, fileName);
        }

        private void GenerateReport(DateTime start, DateTime end)
        {
            Document document = new Document();
            Page page = new Page();
            document.Pages.Add(page);
            string title = "Report for " + start.Month + "/" + start.Day + "/" + start.Year + " - " + end.Month + "/" + end.Day + "/" + end.Year;
            TextArea textArea = new TextArea(title, 100, 0, 400, 30, Font.HelveticaBoldOblique, 18);
            page.Elements.Add(textArea);

            Chart chartNumberOfOffers = getGraphForNumberOfOffers(start, end);
            Chart chartMaxPrices = getGraphForMaxPrices(start, end);
            Chart chartMinPrices = getGraphForMinPrices(start, end);

            page.Elements.Add(chartNumberOfOffers);
            page.Elements.Add(chartMaxPrices);
            page.Elements.Add(chartMinPrices);

            string localPath = "Reports/Tender" + start.Month + "-" + start.Day + "-" + start.Year + "to" + end.Month + "-" + end.Day + "-" + end.Year + ".pdf";
            document.Draw(localPath);
        }

        [HttpGet("offers/number")]
        public IActionResult GetNumberOfOffers(DateTime start, DateTime end)
        {
            Dictionary<string, List<float>> data = service.GetNumberOfOffersForAllTenders(start, end);
            List<string> tenders = new List<string>();
            foreach (Tender tender in service.GetTendersWithOffers())
                if (tender.EndTime >= start && tender.EndTime <= end)
                    tenders.Add(tender.Title);

            var statisticData = new
            {
                data = data,
                tenders = tenders
            };
            return Ok(statisticData);
        }

        [HttpGet("prices/max")]
        public IActionResult GetMaxPrices(DateTime start, DateTime end)
        {
            Dictionary<string, List<float>> data = service.GetMaxPricesForAllTenders(start, end);
            List<string> tenders = new List<string>();
            foreach (Tender tender in service.GetTendersWithOffers())
                if (tender.EndTime >= start && tender.EndTime <= end)
                    tenders.Add(tender.Title);

                var statisticData = new
            {
                data = data,
                tenders = tenders
            };
            return Ok(statisticData);
        }

        [HttpGet("prices/min")]
        public IActionResult GetMinPrices(DateTime start, DateTime end)
        {
            Dictionary<string, List<float>> data = service.GetMinPricesForAllTenders(start, end);
            List<string> tenders = new List<string>();
            foreach (Tender tender in service.GetTendersWithOffers())
                if (tender.EndTime >= start && tender.EndTime <= end)
                    tenders.Add(tender.Title);

            var statisticData = new
            {
                data = data,
                tenders = tenders
            };
            return Ok(statisticData);
        }

        private Chart getGraphForNumberOfOffers(DateTime start, DateTime end)
        {
            Chart chart = new Chart(80, 40, 400, 230);
            PlotArea plotArea = chart.PrimaryPlotArea;

            Dictionary<string, List<float>> data = service.GetNumberOfOffersForAllTenders(start, end);

            Title title1 = new Title("Number of Offers");
            chart.HeaderTitles.Add(title1);

            List<IndexedColumnSeries> columnSeries = new List<IndexedColumnSeries>();

            foreach (string k in data.Keys)
            {
                IndexedColumnSeries newColumnSeries = new IndexedColumnSeries(k);
                newColumnSeries.Values.Add(data[k].ToArray());
                columnSeries.Add(newColumnSeries);
                plotArea.Series.Add(newColumnSeries);
            }

            Title Title = new Title("Number of offers");
            columnSeries[0].YAxis.Titles.Add(Title);
            List<Tender> allTenders = service.GetTendersWithOffers();
            for (int t = 0; t < allTenders.Count; t++)
            {
                if(allTenders[t].EndTime >= start && allTenders[t].EndTime <= end)
                    columnSeries[0].XAxis.Labels.Add(new IndexedXAxisLabel(allTenders.ElementAt(t).Title, t));
            }

            return chart;
        }

        private Chart getGraphForMaxPrices(DateTime start, DateTime end)
        {
            Chart chart = new Chart(80, 270, 400, 230);
            PlotArea plotArea = chart.PrimaryPlotArea;

            Dictionary<string, List<float>> data = service.GetMaxPricesForAllTenders(start, end);

            Title title1 = new Title("Max prices in dollars");
            chart.HeaderTitles.Add(title1);

            List<IndexedColumnSeries> columnSeries = new List<IndexedColumnSeries>();

            foreach (string k in data.Keys)
            {
                IndexedColumnSeries newColumnSeries = new IndexedColumnSeries(k);
                newColumnSeries.Values.Add(data[k].ToArray());
                columnSeries.Add(newColumnSeries);
                plotArea.Series.Add(newColumnSeries);
            }

            Title Title = new Title("Price in dollars");
            columnSeries[0].YAxis.Titles.Add(Title);
            List<Tender> allTenders = service.GetTendersWithOffers();
            for (int t = 0; t < allTenders.Count; t++)
            {
                if (allTenders[t].EndTime >= start && allTenders[t].EndTime <= end)
                    columnSeries[0].XAxis.Labels.Add(new IndexedXAxisLabel(allTenders.ElementAt(t).Title, t));
            }

            return chart;
        }

        private Chart getGraphForMinPrices(DateTime start, DateTime end)
        {
            Chart chart = new Chart(80, 490, 400, 230);
            PlotArea plotArea = chart.PrimaryPlotArea;

            Dictionary<string, List<float>> data = service.GetMinPricesForAllTenders(start, end);

            Title title1 = new Title("Min prices in dollars");
            chart.HeaderTitles.Add(title1);

            List<IndexedColumnSeries> columnSeries = new List<IndexedColumnSeries>();

            foreach (string k in data.Keys)
            {
                IndexedColumnSeries newColumnSeries = new IndexedColumnSeries(k);
                newColumnSeries.Values.Add(data[k].ToArray());
                columnSeries.Add(newColumnSeries);
                plotArea.Series.Add(newColumnSeries);
            }

            Title Title = new Title("Price in dollars");
            columnSeries[0].YAxis.Titles.Add(Title);
            List<Tender> allTenders = service.GetTendersWithOffers();
            for (int t = 0; t < allTenders.Count; t++)
            {
                if (allTenders[t].EndTime >= start && allTenders[t].EndTime <= end)
                    columnSeries[0].XAxis.Labels.Add(new IndexedXAxisLabel(allTenders.ElementAt(t).Title, t));
            }

            return chart;
        }

        [HttpGet("getWholeStatistic")]
        public IActionResult GetWholeStatistic(string pharmacyName)
        {
            List<int> statistic = service.GetPharmacyWinningStatistic(pharmacyName);
            return Ok(statistic);
        }

        [HttpGet("getTendersPharmacyParticipated")]
        public IActionResult GetTendersPharmacyParticipated(string pharmacyName)
        {
            List<Tender> tenders = service.GetTendersPharmacyParticipated(pharmacyName);
            return Ok(tenders);
        }

        [HttpGet("getOffersForTender")]
        public IActionResult GetOffersForTender(string tenderHash, string pharmacyName)
        {
            List<TenderOffer> tenderOffers = service.GetOffersForTender(tenderHash, pharmacyName);
            return Ok(tenderOffers);
        }

        [HttpGet("getWinningOffersForPharmacy")]
        public IActionResult GetWinningOffersForPharmacy(string pharmacyName)
        {
            List<TenderOffer> winningOffers = service.GetWinningOffersForPharmacy(pharmacyName);
            List<WinningOfferDto> winningOffersDto = new List<WinningOfferDto>();
            foreach(TenderOffer offer in winningOffers)
            {
                winningOffersDto.Add(OfferMapper.OfferToWinningOfferDto(offer, service.GetTenderName(offer.TenderOfferHash)));
            }
            return Ok(winningOffersDto);
        }
    }
}