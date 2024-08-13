using ChainOfResponsibiltyDesignPattern.DAL;
using ChainOfResponsibiltyDesignPattern.Models;

namespace ChainOfResponsibiltyDesignPattern.ChainOfResponsibility;

public class Treasurer : Employee
{
    public override void ProcessRequest(CustomerProcessViewModel req)
    {
        Context context = new();
        if (req.Amount <= 100000)
        {
            CustomerProcess customerProcess = new()
            {
                Amount = req.Amount.ToString(),
                Name = req.Name,
                EmployeeName = "Veznedar - Ayşe Çınar",
                Description = "Para Çekme İşlemi Onaylandı, Müşteriye Talep Ettiği Tutar Ödendi"
            };
            context.CustomerProcesses.Add(customerProcess);
            context.SaveChanges();
        }
        else if (NextApprover != null)
        {
            CustomerProcess customerProcess = new()
            {
                Amount = req.Amount.ToString(),
                Name = req.Name,
                EmployeeName = "Veznedar - Ayşe Çınar",
                Description = "Para Çekme Tutarı Veznedarın Günlük Ödeyebileceği Limiti Aştığı İçin İşlem Şube Müdür Yardımcısına Yönlendirildi"
            };
            context.CustomerProcesses.Add(customerProcess);
            context.SaveChanges();
            NextApprover.ProcessRequest(req);
        }
    }
}
