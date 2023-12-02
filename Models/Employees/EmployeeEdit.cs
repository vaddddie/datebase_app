using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sql_web_app.Scripts;
using sql_web_app.Scripts.Tables;


namespace sql_web_app.Models.Employees;

public class EmployeeEditModel : PageModel
{
    public required string[] Atributes;
    public required Dictionary<int, string> JobTitles;
    private readonly ILogger<EmployeeEditModel> _logger;

    public EmployeeEditModel(ILogger<EmployeeEditModel> logger)
    {
        _logger = logger;
    }

    public void OnGet(int id)
    {
        Atributes = EmployeeSQL.GetParamsById(id);
        JobTitles = EmployeeSQL.GetJobTitles();
    }

    public RedirectResult OnPostAccept(int id, int jobTitle_id, string email, string fullname, string password)
    {
        if (password != null)
        {
            EmployeeSQL.Update(id, jobTitle_id, email, fullname, HashClass.Hash(password));
        }
        else
        {
            EmployeeSQL.UpdateWithoutPassword(id, jobTitle_id, email, fullname);
        }

        return Redirect("/Employees");
    }

    public RedirectResult OnPostDelete(int id)
    {
        EmployeeSQL.Delete(id);
        return Redirect("/Employees");
    }
}