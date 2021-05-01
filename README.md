# WeatherFetch
A simple project to get weather info.

# The weather API provider and API key
The weather API provider is [weatherapi.com](https://weatherapi.com). The API key is stored in the secrets file. Its location is:
- on Linux: `~/.microsoft/usersecrets/48c46175-dc22-47b2-81f3-16413325acb5/secrets.json`
- on Windows: `%appdata%\Microsoft\UserSecrets\48c46175-dc22-47b2-81f3-16413325acb5\secrets.json`

The contents look like this:
```json
{
	"Weather:ApiKey": "api key here"
}
```
