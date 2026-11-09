# Lab 3 — Selenium WebDriver (Variant 2)

UI tests for https://the-internet.herokuapp.com/ written in C# with Selenium WebDriver 4 and xUnit.

Pages: Forgot Password, Horizontal Slider, Dropdown, Typos, Entry Ad, File Download, Basic Auth, Dynamic Loading, Context Menu, Redirect Link.

## Run

```
dotnet test Lab3.sln
```

Chrome must be installed; Selenium Manager downloads a matching ChromeDriver. Tests run headless; set `HEADED=1` to see the browser.
Screenshots are saved to `bin/Debug/net9.0/Screenshots` (a copy of the last run is in `Screenshots/`).
