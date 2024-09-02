using ClosedXML.Excel;
using Newtonsoft.Json;
using PersonVerificationApp.Model;
using PersonVerificationApp.Services;

namespace PersonVerificationApp.Controllers
{
    public interface IPersonVerificationService
    {
        Task<string> VerifyFromExcelAsync(IFormFile file);
    }

    public class PersonVerificationService : IPersonVerificationService
    {
        private readonly IVerifyTc _verifyTc;

        public PersonVerificationService(IVerifyTc verifyTc)
        {
            _verifyTc = verifyTc;
        }

        public async Task<string> VerifyFromExcelAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                // Dosya seçilmemişse uygun bir hata mesajı döndürün
                var errorMessage = JsonConvert.SerializeObject(new { error = "Lütfen bir Excel dosyası seçin." });
                return errorMessage;
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheets.First();
                    var people = worksheet.RowsUsed().Skip(1).Select(row => new Person
                    {
                        TcKimlikNo = long.Parse(row.Cell(1).Value.ToString()),
                        Name = row.Cell(2).Value.ToString(),
                        Surname = row.Cell(3).Value.ToString(),
                        YearOfBirth = int.Parse(row.Cell(4).Value.ToString())
                    }).ToList();

                    var verificationResults = await _verifyTc.Check(people);

                    var allMessages = people.Select((person, index) => new
                    {
                        PersonId = person.TcKimlikNo,
                        Name = person.Name,
                        Surname = person.Surname,
                        YearOfBirth = person.YearOfBirth,
                        IsVerified = verificationResults[index]
                    }).ToList();

                    return JsonConvert.SerializeObject(allMessages);
                }
            }
        }


    }
}
