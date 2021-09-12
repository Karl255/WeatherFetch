# WeatherFetch
A simple project to get weather info.
The weather API provider is [weatherapi.com](https://weatherapi.com). Support for multiple APIs is in progress.

# Config and API key
The API key is stored in the config file at `~/.config/weatherfetch.config`. You need use your own API key, otherwise this tool won't work.

The contents look like this:
```json
{
  "wapi-key": "your API key here"
}
```

More features will be added to the config file in the future and new fields will be added automatically.
