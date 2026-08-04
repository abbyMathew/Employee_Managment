using Employee_Managment.Models;

namespace Employee_Managment.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
