namespace JwtAuthorization.Configuration;

public class JwtOptions
{
    public string? Key { get; set; }
    public string? Issuer { get; set; } 
    public string? Audience { get; set; } 
}

/*
 *  "JWT" : {
    "Key" : "MyVerySecretKey_YouShouldStoreThisIn_AzureVault_AwsSecretManager_UserSecrets",
    "Issuer" : "https://localhost:7059",
    "Audience" : "https://localhost:7059"
  }
 *
 * 
 */