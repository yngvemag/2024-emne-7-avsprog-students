namespace OutputCachingBasics.Extensions;

public static class AddOutputCacheExtension
{
    public static void AddOutputCacheBasics(this IServiceCollection services)
    {
        // builder.Services => services
        services.AddOutputCache(options =>
        {
             options.AddBasePolicy(o => o.Expire( TimeSpan.FromMinutes(5)));

             options.AddPolicy("AuthenticatedUserCachePolicy", o =>
             {
                o.SetVaryByHeader("Authorization").Expire(TimeSpan.FromMinutes(5));  
             });
             
             options.AddPolicy("CacheForOneMinute", o => o.Expire(TimeSpan.FromMinutes(1)));
             
             options.AddPolicy("NoCache", o => o.NoCache());
        });
        
        
    }
}