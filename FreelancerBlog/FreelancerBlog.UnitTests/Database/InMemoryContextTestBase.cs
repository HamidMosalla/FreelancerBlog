using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Data.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FreelancerBlog.UnitTests.Database
{
    /// <summary>
    /// Inherit from this type to implement tests
    /// that make use of the in-memory test database
    /// context.
    /// </summary>
    public abstract class InMemoryContextTest : TestBase
    {
        /// <summary>
        /// Gets the in-memory database context.
        /// </summary>
        protected FreelancerBlogContext Context { get; private set; }
        protected UserManager<ApplicationUser> UserManager { get; }

        protected InMemoryContextTest() {
            Context = ServiceProvider.GetService<FreelancerBlogContext>();
            UserManager = ServiceProvider.GetService<UserManager<ApplicationUser>>();

            LoadTestData();
        }

        /// <summary>
        /// Override this method to load test data
        /// into the in-memory database context prior
        /// to any tests being executed in your
        /// test class.
        /// </summary>
        protected virtual void LoadTestData()
        {
        }

        /// <summary>
        /// Override this method to load test data
        /// into the in-memory database context prior
        /// to any tests being executed in your
        /// test class.
        /// FRAGILE: this method can't be called from the constructor so you must call it manually
        /// </summary>
        protected virtual async Task LoadTestDataAsync()
        {
            await Task.CompletedTask;
        }
    }
}