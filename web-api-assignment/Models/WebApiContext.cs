using Microsoft.EntityFrameworkCore;

namespace web_api_assignment.Models
{
    /// <summary>
    /// This is where we seed the data for the database.
    /// </summary>
    public partial class WebApiContext : DbContext
    {
        public WebApiContext() 
        {
        }
        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options) 
        {
        }

    }
}
