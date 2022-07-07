//using Application.Utility;
using AutoMapper;
using ISPoliceAppApi.Data;
using ISPoliceAppApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ISPoliceAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProfileController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _iConfig;
        public ProfileController(ISPoliceAppApiDbContext context, IMapper mapper,
            ILogger<ProfileController> logger, IWebHostEnvironment environment, IConfiguration iConfig)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _hostEnvironment = environment;
            _iConfig = iConfig;
        }
        [HttpGet("GetAssociates")]
        public async Task<ActionResult<IEnumerable<ProfileMasterModel>>> GetAssociates(string name)
        {
            var list = await _context.ProfileMaster.Where(a => a.Name.Contains(name) || string.IsNullOrWhiteSpace(name)).ToListAsync();
            var ProfileMasterList = _mapper.Map<List<ProfileMasterModel>>(list);
            return ProfileMasterList;
        }
        [HttpGet("GetProfile")]
        public async Task<ActionResult<ProfileMasterModel>> GetAssociates(int Id)
        {
            var item = await _context.ProfileMaster.Where(a => a.Id == Id).FirstOrDefaultAsync();
            var ProfileMaster = _mapper.Map<ProfileMasterModel>(item);
            return ProfileMaster;
        }
        [HttpGet("GetAssociatesById")]
        public async Task<ActionResult<IEnumerable<ProfileAssociates>>> GetAssociatesById(int Id)
        {
            var list = await _context.ProfileAssociates.Where(a => a.ProfileId == Id).ToListAsync();
            return list;
        }


        [HttpPost("saveAssociates")]
        public async Task<ActionResult<ProfileAssociates>> AddProfileAssociates(ProfileMasterModel ProfileMaster)
        {
            try
            {
                var entity = _mapper.Map<ProfileMaster>(ProfileMaster);
                _context.ProfileMaster.Add(entity);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetProfile", new { id = ProfileMaster.Id }, ProfileMaster);
            }
            catch (Exception ex)
            {
                _logger.LogError("error", ex);
                throw;
            }
        }

        [HttpPost("save")]
        public async Task<ActionResult<ProfileMaster>> save(ProfileMaster profileMaster)
        {
            try
            {
                //var entity = _mapper.Map<ProfileMaster>(ProfileMaster);
                _context.ProfileMaster.Add(profileMaster);
                await _context.SaveChangesAsync();
                //foreach (var item in profileMaster.ProfileAssociates)
                //{
                //    item.AssociatesId = profileMaster.Id;
                //}
                //_context.UpdateRange(profileMaster.ProfileAssociates);
                //await _context.SaveChangesAsync();
                return profileMaster;

            }
            catch (Exception ex)
            {
                _logger.LogError("error", ex);
                throw;
            }
        }

        [HttpPost("addQuickProfile")]
        public async Task<ActionResult<ProfileMaster>> addQuickProfile(ProfileMasterModel profileMasterModel)
        {
            try
            {
                ProfileMaster profileMaster = new ProfileMaster();
                profileMaster.Id = profileMasterModel.Id;
                profileMaster.Name = profileMasterModel.Name;
                profileMaster.Hs = profileMasterModel.Hs;
                profileMaster.IsActive = profileMasterModel.IsActive;
                profileMaster.ProfileAlias = profileMasterModel.ProfileAlias;
                profileMaster.EntryDate = System.DateTime.Now;
                _context.ProfileMaster.Add(profileMaster);
                await _context.SaveChangesAsync();
                return profileMaster;
            }
            catch (Exception ex)
            {
                _logger.LogError("error", ex);
                throw;
            }


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileMaster>> GetProfileMaster(int id)
        {
            //var controlRoomDSR = await _context.ControlRoomDSR.FindAsync(id);
            var controlRoomDSR = await _context.ProfileMaster.Include(x => x.CaseDetails)
                .Include(y => y.ProfileAbstracts)
                .Include(y => y.ProfileAlias)
                .Include(y => y.ProfileAssociates)
                .Include(y => y.ProfileChildrens)
                .Include(y => y.ProfileSiblings)
                .Include(y => y.ProfileSpouses)
                .Include(y => y.ProfileTransaction)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (controlRoomDSR == null)
            {
                return NotFound();
            }
            return controlRoomDSR;
        }
        [HttpGet("profileList")]
        public async Task<ActionResult<List<ProfileMaster>>> profileList()
        {
            //var controlRoomDSR = await _context.ControlRoomDSR.FindAsync(id);
            var controlRoomDSR = await _context.ProfileMaster.Include(x => x.CaseDetails).Include(a=>a.ProfileTransaction)
                .Include(y => y.ProfileAlias).Where(a => a.IsActive == true).ToListAsync();
            return controlRoomDSR;
        }

        [HttpGet("getAssociateList/{id}")]
        public async Task<ActionResult<IList<ProfileAssociates>>> getAssociateList(int id)
        {
            //var controlRoomDSR = await _context.ControlRoomDSR.FindAsync(id);
            var controlRoomDSR = await _context.ProfileAssociates.Include(x => x.AssociatesProfileDetail).ThenInclude(y => y.ProfileAlias)
               .Where(a => a.IsActive && a.ProfileId == id)
                .ToListAsync();
            if (controlRoomDSR == null)
            {
                return NotFound();
            }
            return controlRoomDSR;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(int id, ProfileMaster profileMaster)
        {
            if (id != profileMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(profileMaster).State = EntityState.Modified;

            //_context.ProfileMaster.Update(profileMaster);

            foreach (var profileCase in profileMaster.CaseDetails)
            {
                _context.Update(profileCase);
            }
            foreach (var abstracts in profileMaster.ProfileAbstracts)
            {
                _context.Update(abstracts);
            }

            foreach (var alias in profileMaster.ProfileAlias)
            {
                _context.Update(alias);
            }

            foreach (var associates in profileMaster.ProfileAssociates)
            {
                _context.Update(associates);
            }
            foreach (var childrens in profileMaster.ProfileChildrens)
            {
                _context.Update(childrens);
            }
            foreach (var item in profileMaster.ProfileSiblings)
            {
                _context.Update(item);
            }

            foreach (var item in profileMaster.ProfileSpouses)
            {
                _context.Update(item);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("error", ex);
                if (!ProfileRxists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool ProfileRxists(int id)
        {
            return _context.ProfileMaster.Any(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProfileMaster>> DeleteProfile(int id)
        {
            var profile = await _context.ProfileMaster.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            profile.IsActive = false;
            _context.Entry(profile).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return profile;
        }
        [HttpGet("downloaddocument/{id}")]
        public async Task<ActionResult> downloaddocument(int id)
        {
            try
            {
                var profileMaster = await _context.ProfileMaster.Include(x => x.CaseDetails)
                .Include(y => y.ProfileAbstracts)
                .Include(y => y.ProfileAlias)
                .Include(y => y.ProfileAssociates).ThenInclude(b => b.AssociatesProfileDetail)
                .Include(y => y.ProfileChildrens)
                .Include(y => y.ProfileSiblings)
                .Include(y => y.ProfileSpouses)
                .Include(y => y.ProfileTransaction)
                .FirstOrDefaultAsync(a => a.Id == id);
                var fileToDownload = downloadWord(profileMaster);

                var memory = new MemoryStream();
                using (var stream = new FileStream(fileToDownload, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, GetContentType(fileToDownload));
            }
            catch (Exception ex)
            {
                _logger.LogError("error", ex);

                throw ex;
            }
            
           
        }
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
        private string downloadWord(ProfileMaster obj)
        {
            string samplePath = Path.GetFullPath(_hostEnvironment.ContentRootPath + "/Resources/Template/ProfileTemplate.docx");
            string docxPath = Path.GetFullPath(_hostEnvironment.ContentRootPath + "/Resources/Template/Profile" +  obj.Name + ".docx");

            var document = new Document();
            document.LoadFromFile(samplePath);
            document.Watermark = null;
            Section section = document.Sections[0];
            if (string.IsNullOrWhiteSpace(obj.Image))
            {
                obj.Image = @"Resources\Images\download.png";
            }

            Image image = Image.FromFile(_hostEnvironment.ContentRootPath + "\\" + obj.Image);


            Table table = section.Tables[0] as Table;
            TableCell cell1 = table.Rows[0].Cells[0];
            Paragraph p1 = cell1.Paragraphs[0];
            DocPicture pic = p1.AppendPicture(image);
            pic.Height = pic.Height * 0.7f;
            pic.Width = pic.Width * 0.7f;
            //pic.HorizontalPosition = 50.0F;
            //pic.VerticalPosition = 60.0F;

            Dictionary<string, string> dictReplace = GetReplaceDictionary(obj, document);
            foreach (KeyValuePair<string, string> kvp in dictReplace)
            {
                document.Replace(kvp.Key, kvp.Value, true, true);
            }
            //return document;
            document.SaveToFile(docxPath, FileFormat.Docx);
            ////Convert to PDF  
            ////document.SaveToFile(pdfPath, FileFormat.PDF);
            document.Close();
            return docxPath;
        }
        Dictionary<string, string> GetReplaceDictionary(ProfileMaster obj, Document doc)
        {
            Dictionary<string, string> replaceDict = new Dictionary<string, string>();

            replaceDict.Add("#HS#", obj.Hs);
            var categoryList = _iConfig.GetSection("CategoryList")
                   .GetChildren()
                   .ToList()
                   .Select(x => new
                   {
                       Id = x.GetValue<string>("Id"),
                       Name = x.GetValue<string>("Name"),
                   });
            var statusList = _iConfig.GetSection("StatusList")
                   .GetChildren()
                   .ToList()
                   .Select(x => new
                   {
                       Id = x.GetValue<string>("Id"),
                       Name = x.GetValue<string>("Name"),
                   });
            replaceDict.Add("#NAME#", obj.Name);
            var children = "";
            if (obj.ProfileChildrens.Where(a => a.Gender == 1).Count() > 1)
            {
                children = obj.ProfileChildrens.Where(a => a.Gender == 1).Count().ToString() + " Daughter";
            }
            if (obj.ProfileChildrens.Where(a => a.Gender == 0).Count() > 1)
            {
                children = children + obj.ProfileChildrens.Where(a => a.Gender == 1).Count().ToString() + " son";
            }
            replaceDict.Add("#CHILDREN#", children);
            var siblings = "";
            var siblingsList = obj.ProfileSiblings.Where(a => a.relation == "1").ToList();
            if (siblingsList.Count > 0)
            {
                siblings = siblingsList.Count > 1 ? siblingsList.Count + " Brothers" : siblingsList.Count + " Brother";
            }
            siblingsList = obj.ProfileSiblings.Where(a => a.relation == "0").ToList();
            if (siblingsList.Count > 0)
            {
                siblings = siblingsList.Count > 1 ? siblings + siblingsList.Count + " Sisters" : siblings + siblingsList.Count + " Sister";
            }
            replaceDict.Add("#SIBLINGS#", siblings);
            if (obj.ProfileTransaction != null)
            {
                replaceDict.Add("#CATEGORY#", categoryList.First(a => a.Id == obj.ProfileTransaction.Category.ToString()).Name);

                replaceDict.Add("#AGE#", obj.ProfileTransaction.Age > 0 ? obj.ProfileTransaction.Age.ToString() : "");
                replaceDict.Add("#ADDRESS#", obj.ProfileTransaction.PresentAddress);
                replaceDict.Add("#STATUS#", obj.ProfileTransaction.Status != null ? statusList.First(a => a.Id == obj.ProfileTransaction.Status.ToString()).Name : "Nil");
                replaceDict.Add("#FNAME#", obj.ProfileTransaction.FatherName);
                replaceDict.Add("#MOTHER#", obj.ProfileTransaction.MotherName);
                replaceDict.Add("#PADDRESS#", obj.ProfileTransaction.PresentAddress);
                var mred = obj.ProfileTransaction.MartialStatus == 0 ? "" : obj.ProfileTransaction.MartialStatus == 1 ? "Married " : "Umarried";
                replaceDict.Add("#MRED#", mred);
                replaceDict.Add("#SPOUSENAME#", obj.ProfileTransaction.SpouseName);

                replaceDict.Add("#EDUCATION#", obj.ProfileTransaction.Education);
                replaceDict.Add("#JOB#", obj.ProfileTransaction.Occupation);
                replaceDict.Add("#GOONDAS#", obj.ProfileTransaction.NoOfGoondas);
                replaceDict.Add("#SPROCEEDING#", obj.ProfileTransaction.SecurityProceeding);
                replaceDict.Add("#DATE#", obj.ProfileTransaction.dateOfInitiation != DateTime.MinValue ? "Nil" : obj.ProfileTransaction.dateOfInitiation.Value.ToString("D"));
                replaceDict.Add("#LAT#", obj.ProfileTransaction.LastAction);
                replaceDict.Add("#LATDATE#", obj.ProfileTransaction.LastActionDate != DateTime.MinValue ? "Nil" : obj.ProfileTransaction.LastActionDate.Value.ToString("D"));
                replaceDict.Add("#BAIL#", obj.ProfileTransaction.Bail);
                replaceDict.Add("#BAILD#", obj.ProfileTransaction.BailDate != DateTime.MinValue ? "Nil" : obj.ProfileTransaction.BailDate.Value.ToString("D"));
            }
            else
            {
                replaceDict.Add("#CATEGORY#", "Nil");
                replaceDict.Add("#AGE#", "Nil");
                replaceDict.Add("#STATUS#", "Nil");
                replaceDict.Add("#ADDRESS#", "Nil");
                replaceDict.Add("#FNAME#", "Nil");
                replaceDict.Add("#MOTHER#", "Nil");
                replaceDict.Add("#PADDRESS#", "Nil");
                replaceDict.Add("#MRED#", "Nil");
                replaceDict.Add("#SPOUSENAME#", "Nil");
                //replaceDict.Add("#EDUCATION#", "");
                replaceDict.Add("#JOB#", "Nil");
                replaceDict.Add("#GOONDAS#", "Nil");
                replaceDict.Add("#SPROCEEDING#", "Nil");
                replaceDict.Add("#EDUCATION#", "Nil");
                replaceDict.Add("#DATE#", "Nil");
                replaceDict.Add("#LAT#", "Nil");
                replaceDict.Add("#LATDATE#", "Nil");
                replaceDict.Add("#BAIL#", "Nil");
                replaceDict.Add("#BAILD#", "Nil");
            }
            var ASSOCIATE = "";
            var ASSOCIATEHSNO = "";
            var associateindex = 0;
            foreach (var item in obj.ProfileAssociates)
            {
                associateindex++;
                ASSOCIATE =  ASSOCIATE + associateindex + " " + item.AssociatesProfileDetail.Name + "\n";
                ASSOCIATEHSNO = ASSOCIATEHSNO + associateindex+  item.AssociatesProfileDetail.Hs + "\n";
            }
            replaceDict.Add("#ASSOCIATE#", ASSOCIATE);
            replaceDict.Add("#HSNO#", ASSOCIATEHSNO);
            Table abstractTable = doc.Sections[0].Tables[2] as Table;
            var abstractTableStartRow = 2;
            foreach (var item in obj.ProfileAbstracts)
            {
                TableRow DataRow = abstractTable.AddRow();
                abstractTable.Rows.Insert(abstractTable.Rows.Count - 1, DataRow);
                //abstractTable.ResetCells(abstractTable.Rows.Count, 0);
                //TableRow  = .Rows[abstractTableStartRow];
                //abstractTable.ResetCells(4, 4);
                DataRow.Height = 20;
                // District Data
                DataRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph p2 = DataRow.Cells[0].AddParagraph();
                var DistCityMessage = "";
                switch (item.DistCity)
                {
                    case 0:
                        DistCityMessage = "Case involved With in City";
                        break;
                    case 1:
                        DistCityMessage = "Other Dist. Within City ";
                        break;
                    case 2:
                        DistCityMessage = "Other than City ";
                        break;
                    default:
                        break;
                }
                TextRange TR2 = p2.AppendText(DistCityMessage);
                //Murder call
                DataRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph Murder = DataRow.Cells[1].AddParagraph();
                TextRange MurderText = Murder.AppendText(item.Murder.ToString());

                DataRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph attemptMurder = DataRow.Cells[2].AddParagraph();
                TextRange attemptMurderText = attemptMurder.AppendText(item.AttmptMurder.ToString());

                DataRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph Ndps = DataRow.Cells[3].AddParagraph();
                TextRange NdpsText = Ndps.AppendText(item.Ndps.ToString());

                DataRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph robbery = DataRow.Cells[4].AddParagraph();
                TextRange robberyText = robbery.AppendText(item.Robbery.ToString());

                DataRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph chainsnatch = DataRow.Cells[5].AddParagraph();
                TextRange chainsnatchText = chainsnatch.AppendText(item.ChainSnatch.ToString());

                DataRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph mobilesnatch = DataRow.Cells[6].AddParagraph();
                TextRange mobilesnatchText = mobilesnatch.AppendText(item.MobileSnatch.ToString());

                DataRow.Cells[7].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph hbDay = DataRow.Cells[7].AddParagraph();
                TextRange hbDayText = hbDay.AppendText(item.HbDay.ToString());

                DataRow.Cells[8].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph hbNight = DataRow.Cells[8].AddParagraph();
                TextRange hbNightText = hbNight.AppendText(item.HbNight.ToString());

                DataRow.Cells[9].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph otherCase = DataRow.Cells[9].AddParagraph();
                TextRange otherCaseText = otherCase.AppendText(item.OtherCase.ToString());

                DataRow.Cells[10].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph technicalCase = DataRow.Cells[10].AddParagraph();
                TextRange technicalCaseText = technicalCase.AppendText(item.TechCase.ToString());

                DataRow.Cells[11].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph total = DataRow.Cells[11].AddParagraph();
                TextRange totalText = total.AppendText((item.AttmptMurder + item.Murder + item.Ndps + item.Robbery + item.ChainSnatch + item.MobileSnatch + item.HbDay + item.HbNight + item.OtherCase + item.TechCase).ToString());
            }
            #region Adding total row
            TableRow DataRowTotal = abstractTable.AddRow();
            DataRowTotal.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            Paragraph DataRowTotalp2 = DataRowTotal.Cells[0].AddParagraph();

            TextRange TR2Total = DataRowTotalp2.AppendText("Total");
            //Murder call
            DataRowTotal.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            Paragraph TotalMurder = DataRowTotal.Cells[1].AddParagraph();
            TextRange TotalMurderText = TotalMurder.AppendText(obj.ProfileAbstracts.Sum(a => a.Murder).ToString());

            DataRowTotal.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            Paragraph TotalattemptMurder = DataRowTotal.Cells[2].AddParagraph();
            TextRange TotalattemptMurderText = TotalattemptMurder.AppendText(obj.ProfileAbstracts.Sum(a => a.AttmptMurder).ToString());

            DataRowTotal.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            Paragraph TotalNdps = DataRowTotal.Cells[3].AddParagraph();
            TextRange TotalNdpsText = TotalNdps.AppendText(obj.ProfileAbstracts.Sum(a => a.Ndps).ToString());

            DataRowTotal.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            Paragraph Totalrobbery = DataRowTotal.Cells[4].AddParagraph();
            TextRange TotalrobberyText = Totalrobbery.AppendText(obj.ProfileAbstracts.Sum(a => a.Robbery).ToString());

            DataRowTotal.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            Paragraph Totalchainsnatch = DataRowTotal.Cells[5].AddParagraph();
            TextRange TotalchainsnatchText = Totalchainsnatch.AppendText(obj.ProfileAbstracts.Sum(a => a.ChainSnatch).ToString());

            DataRowTotal.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            Paragraph Totalmobilesnatch = DataRowTotal.Cells[6].AddParagraph();
            TextRange TotalmobilesnatchText = Totalmobilesnatch.AppendText(obj.ProfileAbstracts.Sum(a => a.MobileSnatch).ToString());

            DataRowTotal.Cells[7].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            Paragraph TotalhbDay = DataRowTotal.Cells[7].AddParagraph();
            TextRange TotalhbDayText = TotalhbDay.AppendText(obj.ProfileAbstracts.Sum(a => a.HbDay).ToString());

            DataRowTotal.Cells[8].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            Paragraph TotalhbNight = DataRowTotal.Cells[8].AddParagraph();
            TextRange TotalhbNightText = TotalhbNight.AppendText(obj.ProfileAbstracts.Sum(a => a.HbNight).ToString());

            DataRowTotal.Cells[9].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            Paragraph TotalotherCase = DataRowTotal.Cells[9].AddParagraph();
            TextRange TotalotherCaseText = TotalotherCase.AppendText(obj.ProfileAbstracts.Sum(a => a.OtherCase).ToString());

            DataRowTotal.Cells[10].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            Paragraph TotaltechnicalCase = DataRowTotal.Cells[10].AddParagraph();
            TextRange TotaltechnicalCaseText = TotaltechnicalCase.AppendText(obj.ProfileAbstracts.Sum(a => a.TechCase).ToString());

            DataRowTotal.Cells[11].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            Paragraph Totaltotal = DataRowTotal.Cells[11].AddParagraph();
            TextRange TotaltotalText = Totaltotal.AppendText((obj.ProfileAbstracts.Sum(a => a.AttmptMurder) + obj.ProfileAbstracts.Sum(a => a.Murder) + obj.ProfileAbstracts.Sum(a => a.Ndps) + obj.ProfileAbstracts.Sum(a => a.Robbery) +
                obj.ProfileAbstracts.Sum(a => a.ChainSnatch) + obj.ProfileAbstracts.Sum(a => a.MobileSnatch) + obj.ProfileAbstracts.Sum(a => a.HbDay) + obj.ProfileAbstracts.Sum(a => a.HbNight) + obj.ProfileAbstracts.Sum(a => a.OtherCase) + obj.ProfileAbstracts.Sum(a => a.TechCase)).ToString());

            #endregion
            Table caseDetailsTable = doc.Sections[0].Tables[3] as Table;
            int index = 0;
            var currentPsList = obj.CaseDetails.Select(a => a.ps).ToList();
            var PSList = this._context.StationMaster.Where(a => currentPsList.Contains(a.StationId)).ToList();
            foreach (var item in obj.CaseDetails)
            {
                index++;
                TableRow DataRow = caseDetailsTable.AddRow();
                caseDetailsTable.Rows.Insert(caseDetailsTable.Rows.Count - 1, DataRow);
                DataRow.Height = 20;
                // District Data
                DataRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph pIndex = DataRow.Cells[0].AddParagraph();
                pIndex.AppendText(index.ToString());
                //Murder call
                DataRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph PS = DataRow.Cells[1].AddParagraph();
                PS.AppendText(PSList.Where(a => a.StationId == item.ps).Select(b => b.StationName).FirstOrDefault());

                DataRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph crNoSec = DataRow.Cells[2].AddParagraph();
                crNoSec.AppendText(item.cr.ToString() + "-" + item.Section.ToString());

                DataRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph head = DataRow.Cells[3].AddParagraph();
                head.AppendText(item.Head.ToString());

                DataRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph Io = DataRow.Cells[4].AddParagraph();
                Io.AppendText(item.io.ToString());

                DataRow.Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph court = DataRow.Cells[5].AddParagraph();
                court.AppendText(item.Court.ToString());

                DataRow.Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph Goondas = DataRow.Cells[6].AddParagraph();
                Goondas.AppendText(item.Goondas.ToString());

                DataRow.Cells[7].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph Stage = DataRow.Cells[7].AddParagraph();
                Stage.AppendText(item.Stage.ToString());

                DataRow.Cells[8].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph Reason = DataRow.Cells[8].AddParagraph();
                Reason.AppendText(item.Reason.ToString());


            }

            return replaceDict;
        }

        [HttpGet("test")]
        public async Task<ActionResult<string>> testScript(int id)
        {
            //var controlRoomDSR = await _context.ControlRoomDSR.FindAsync(id);
            try
            {
                _logger.LogInformation("starts the execution");
                //I had to comment this util to surpress error
                /* PythonUtil obj = new PythonUtil();
                 var result = obj.Run(@"E:\Sumesh\Sample\isp\ISPoliceAppApi\Resources\Pythontest.py", "5");
                 _logger.LogInformation(result);*/
                var result = "";

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("error", ex);
                throw ex;
            }

        }

        [HttpGet("heartbeat")]
        public async Task<ActionResult<string>> heartbeat()
        {
            //var controlRoomDSR = await _context.ControlRoomDSR.FindAsync(id);
            return "heartbeat";

        }

    }

}
