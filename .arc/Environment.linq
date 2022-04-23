<Query Kind="Expression" />

Enum.GetValues(typeof(Environment.SpecialFolder))
    .Cast<Environment.SpecialFolder>()
    .Select(specialFolder => new
    {
        Name = specialFolder.ToString(),
        Path = Environment.GetFolderPath(specialFolder)
    })
    .OrderBy(item => item.Path.ToLower())