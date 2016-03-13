using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataService.Models;
using CentralDataService.DTO;
using Newtonsoft.Json.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net.Http.Headers;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Web.Http.Cors;

namespace CentralDataService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReportsController : ApiController
    {
        private DataServiceDbContext db = new DataServiceDbContext();

        // GET: api/Reports
        //public IQueryable<Report> GetReports()
        //{
        //    return db.Reports;
        //}

       
        [ActionName("GetReportDTOs")]
        public IEnumerable<ReportDTO> GetReports()
        {
            var allReports = db.Reports.ToList(); 
            var result = new List<ReportDTO>();
           foreach(var report in allReports)
            {
                var reportDTO = new ReportDTO();
                reportDTO.CreatedDate = report.CreatedDate;
                reportDTO.ReportId = report.Id;
                JObject root = JObject.Parse(report.JsonContent);

                reportDTO.Name  = (string)root["first_name"] + ' ' + (string)root["last_name"];
                result.Add(reportDTO);
            }
            return result;
        }






        // GET: api/Reports/5

        public HttpResponseMessage GetReport(int id)
        {
            var report = db.Reports.Where(p => p.Id == id).SingleOrDefault();


            var reportDTO = new ReportDTO();
            reportDTO.CreatedDate = report.CreatedDate;
            reportDTO.ReportId = report.Id;
            JObject root = JObject.Parse(report.JsonContent);

            reportDTO.Name = (string)root["first_name"] + ' ' + (string)root["last_name"];

            //var pdfString = string.Format("CLient {0} at Address {1} has the following items listed {2}", reportDTO.Name, (string)root["Address"], (string)root["componentList"].ToString());
            var pdfString = JsonHelper.FormatJson(report.JsonContent);

           
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(pdfString);
            //a text file is actually an octet-stream (pdf, etc)
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            //we used attachment to force download
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "file.txt";
            return result;
        }

      
        // PUT: api/Reports/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReport(int id, Report report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != report.Id)
            {
                return BadRequest();
            }

            db.Entry(report).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Reports
        [ResponseType(typeof(Report))]
        public IHttpActionResult PostReport(Report report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            report.CreatedDate = DateTime.Now;

            db.Reports.Add(report);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = report.Id }, report);
        }

        // DELETE: api/Reports/5
        [ResponseType(typeof(Report))]
        public IHttpActionResult DeleteReport(int id)
        {
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return NotFound();
            }

            db.Reports.Remove(report);
            db.SaveChanges();

            return Ok(report);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReportExists(int id)
        {
            return db.Reports.Count(e => e.Id == id) > 0;
        }
    }

    public class JsonHelper
    {
        private const string INDENT_STRING = "    ";
        public static string FormatJson(string str)
        {
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = false;
                        var index = i;
                        while (index > 0 && str[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }
    }

    static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (var i in ie)
            {
                action(i);
            }
        }
    }
}