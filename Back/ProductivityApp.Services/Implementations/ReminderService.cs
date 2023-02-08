using OfficeOpenXml;
using ProducitivityApp.DataAccess.Interfaces;
using ProductivityApp.Domain.Entities;
using ProductivityApp.Dtos.ReminderDtos;
using ProductivityApp.Mappers.ReminderMappers;
using ProductivityApp.Services.Interfaces;
using ProductivityApp.Shared.CustomReminderExceptions;
using ProductivityApp.Shared.CustomUserExceptions;
using Serilog;
using System.Web.Http;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Task = System.Threading.Tasks.Task;

namespace ProductivityApp.Services.Implementations
{
    public class ReminderService : IReminderService
    {

        private readonly IReminderRepository _reminderRepository;
        private readonly IUserRepository _userRepository;

        public ReminderService(IReminderRepository reminderRepository, IUserRepository userRepository)
        {
            _reminderRepository = reminderRepository;
            _userRepository = userRepository;

        }

        public async Task AddReminder(AddReminderDto addReminderDto, int userId)
        {
            User userDb = await _userRepository.GetById(userId);
            if (userDb == null)
            {
                Log.Logger.Error($"The user id entered {userId} was not found entered by");
                throw new UserNotFoundException($"User with id {userId} was not found in the database.");
            }
            if (string.IsNullOrEmpty(addReminderDto.ReminderTitle))
            {
                throw new ReminderDataException($"Reminder Title must not be empty!");
            }
            if (string.IsNullOrEmpty(addReminderDto.ReminderTime))
            {
                throw new ReminderDataException($"Reminder Time must not be empty!");
            }
            if (string.IsNullOrEmpty(addReminderDto.ReminderDate))
            {
                throw new ReminderDataException($"Reminder Date must not be empty!");
            }
            Reminder newReminder = addReminderDto.ToReminder();
            newReminder.UserId = userId;
            Log.Logger.Information($"User {userDb.Username} added the reminder {newReminder.ReminderTitle}");
            await _reminderRepository.Add(newReminder);

        }

        public async Task DeleteReminder(int id)
        {
            Reminder reminderDb = await _reminderRepository.GetById(id);
            if (reminderDb == null)
            {
                throw new ReminderNotFoundException($"User with id {id} was not found in the database.");
            }
            await _reminderRepository.Delete(reminderDb);
        }

        public async Task<List<ReminderDto>> GetAllReminders(int userId)
        {
            List<Reminder> remindersDb = await _reminderRepository.GetAll();

            List<ReminderDto> remindersDto = remindersDb.Where(x => x.UserId == userId).Select(s => s.ToReminderDto()).ToList();
            return remindersDto;
        }

        public async Task<List<ReminderDto>> GetAllReminderWithPagination(int userId, int pageNum, int pageSize)
        {
            List<Reminder> reminderDb = await _reminderRepository.GetAll();


            if (pageNum != 0 && pageSize != 0)
            {
                reminderDb = reminderDb.Skip((pageNum - 1) * pageSize)
                .Take(pageSize).ToList();
            };

            return reminderDb.Where(x => x.UserId == userId).Select(o => ReminderMapper.ToReminderDto(o)).ToList();
        }

        public async Task<ReminderDto> GetReminderById(int id)
        {

            Reminder reminderDb = await _reminderRepository.GetById(id);
            if(reminderDb == null)
            {
                throw new ReminderNotFoundException($"Reminder with the id {id} was not found in the database");
            }

            ReminderDto reminderDto = reminderDb.ToReminderDto();
            return reminderDto;
        }

        public async Task MakeOfficeDocForReminder(int id)
        {

            throw new NotImplementedException();
            //List<Reminder> reminderDb = await _reminderRepository.GetAll();

            //using (ExcelPackage package = new ExcelPackage())
            //{
            //    var worksheet = package.Workbook.Worksheets.Add("Sheet1");
            //    worksheet.DefaultColWidth = 18;
            //    worksheet.Cells.Style.WrapText = true;
            //    worksheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            //    worksheet.Cells["A1"].Value = "Name";

            //    for (int i = 2; i < reminderDb.Count() + 2; i++)
            //    {
            //        worksheet.Cells["A" + i].Value = reminderDb[i - 2].ReminderTitle;
            //    }

                //using (var package = new ExcelPackage())
                //{
                //    ExcelWorksheet sheet = package.Workbook.Worksheets.Add("MySheet");

                //    // One cell
                //    ExcelRange cellA2 = sheet.Cells["A2"];
                //    var alsoCellA2 = sheet.Cells[2, 1];
                //    Assert.That(cellA2.Address, Is.EqualTo("A2"));
                //    Assert.That(cellA2.Address, Is.EqualTo(alsoCellA2.Address));

                //    // Column from a cell
                //    // ExcelRange.Start is the top and left most cell
                //    Assert.That(cellA2.Start.Column, Is.EqualTo(1));
                //    // To really get the column: sheet.Column(1)

                //    // A range
                //    ExcelRange ranger = sheet.Cells["A2:C5"];
                //    var sameRanger = sheet.Cells[2, 1, 5, 3];
                //    Assert.That(ranger.Address, Is.EqualTo(sameRanger.Address));

                //    //sheet.Cells["A1,A4"] // Just A1 and A4
                //    //sheet.Cells["1:1"] // A row
                //    //sheet.Cells["A:B"] // Two columns

                //    // Linq
                //    var l = sheet.Cells["A1:A5"].Where(range => range.Comment != null);

                //    // Dimensions used
                //    Assert.That(sheet.Dimension, Is.Null);

                //    ranger.Value = "pushing";
                //    var usedDimensions = sheet.Dimension;
                //    Assert.That(usedDimensions.Address, Is.EqualTo(ranger.Address));

                //    // Offset: down 5 rows, right 10 columns
                //    var movedRanger = ranger.Offset(5, 10);
                //    Assert.That(movedRanger.Address, Is.EqualTo("K7:M10"));
                //    movedRanger.Value = "Moved";

                //    package.SaveAs(new FileInfo(@""));
                //}

                //Session["DownloadExcel_FileManager"] = package.GetAsByteArray();
                //return Json("", JsonRequestBehavior.AllowGet);
            //}
        }

        //[HttpGet]
        //public ActionResult DownloadExcel(string name)
        //{
        //    if (UserSession["DownloadExcel_FileManager"] != null)
        //    {
        //        byte[] data = UserSession["DownloadExcel_FileManager"] as byte[];
        //        return File(data, "application/octet-stream", name + ".xlsx");
        //    }
        //    else
        //    {
        //        return new EmptyResult();
        //    }
        //}


        public async Task UpdateReminder(UpdateReminderDto updateReminderDto)
        {
            Reminder reminderDb = await _reminderRepository.GetById(updateReminderDto.Id);

            if(reminderDb == null)
            {
                throw new ReminderNotFoundException($"Reminder with the id {updateReminderDto.Id} was not found");
            }

            User userDb = await _userRepository.GetById(updateReminderDto.UserId);
            if (userDb == null)
            {
                throw new UserNotFoundException($"User with id {updateReminderDto.UserId} was not found in the database.");
            }
            if (string.IsNullOrEmpty(updateReminderDto.ReminderTitle))
            {
                throw new ReminderDataException($"Reminder Name must not be empty!");
            }
            if (string.IsNullOrEmpty(updateReminderDto.ReminderTime))
            {
                throw new ReminderDataException($"Reminder Time must not be empty!");
            }
            if (string.IsNullOrEmpty(updateReminderDto.ReminderDate))
            {
                throw new ReminderDataException($"Reminder Date must not be empty!");
            }

            updateReminderDto.UpdateDbReminder(reminderDb);
            await _reminderRepository.Update(reminderDb);
        }
    }
}
