namespace prototype_csharp_alchemy_helper_domain;

using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using prototype_csharp_alchemy_helper_datastore;

enum DataSets
{
    morrowind,
    oblivion
}

enum Level
{
    apprentice = 1,
    journeyman = 2,
    expert = 3,
    master = 4
}

public class RequestBasedMediatorFactory : IMediatorFactory
{
    private readonly ILogger<IMediator> _mediatorLogger;

    public RequestBasedMediatorFactory(ILogger<IMediator> logger)
    {
        this._mediatorLogger = logger;
    }

    public IMediator GetMediator(string? urlPath)
    {
        try
        {
            if (urlPath == null)
            {
                // Healthcheck path? Log a warning and just return default ~\_:)_/~
                this._mediatorLogger.LogWarning($"The [{nameof(urlPath)}] is null; defaulting!");
                
                return new StaticDictionaryMediator(this._mediatorLogger);
            }

            string language = nameof(language),
                version = nameof(version),
                dataset = nameof(dataset),
                entity = nameof(entity),
                level = nameof(level),
                function = nameof(function),
                method = nameof(method),
                querystring = nameof(querystring);

            string pattern = @$"\/(?<{language}>csapi)\/(?<{version}>v\d)\/(?<{dataset}>[a-zA-Z]+)\/(?<{entity}>potions|ingredients)(\/(?<{level}>[a-zA-Z]+))?(\/(?<{function}>recipes))?\/(?<{method}>with-effects)(?<{querystring}>\?.*)?";
            Regex matcher = new Regex(pattern);
            Match match = matcher.Match(urlPath);

            if (match == null || string.IsNullOrEmpty(match.Value))
            {
                this._mediatorLogger.LogWarning($"The [{nameof(matcher)}] didn't recognise the path; defaulting!");
                
                return new StaticDictionaryMediator(this._mediatorLogger);
            }

            Group? datasetGroup = match.Groups[dataset];

            if (datasetGroup == null)
            {
                // Healthcheck path? Just return default ~\_:)_/~
                this._mediatorLogger.LogWarning($"The [{nameof(datasetGroup)}] [{dataset}] is null; defaulting!");
                
                return new StaticDictionaryMediator(this._mediatorLogger);
            }

            DataSets requestDataset = default;

            if (Enum.TryParse<DataSets>(datasetGroup.Value.ToLower(), out requestDataset))
            {
                switch (requestDataset)
                {
                    case DataSets.morrowind: return new StaticDictionaryMediator(this._mediatorLogger, new MorrowindRepo());
                    case DataSets.oblivion:
                    {
                        Group? levelGroup = match.Groups[level];
                        Level requestLevel = default;

                        if (levelGroup == null)
                        {
                            this._mediatorLogger.LogWarning($"The [{nameof(levelGroup)}] [{level}] is null; defaulting!");
                            
                            requestLevel = Level.apprentice;
                        }
                        else if (!Enum.TryParse<Level>(levelGroup.Value.ToLower(), out requestLevel))
                        {
                            this._mediatorLogger.LogWarning($"The [{nameof(levelGroup)}] [{levelGroup.Value}] could not be parsed; defaulting!");
                
                            requestLevel = Level.apprentice;
                        }

                        switch (requestLevel)
                        {
                            case Level.apprentice:
                            case Level.journeyman:
                            case Level.expert:
                            case Level.master:
                                return new StaticDictionaryMediator(this._mediatorLogger, new OblivionRepo((int)requestLevel));
                            default:
                            {
                                this._mediatorLogger.LogWarning($"The [{nameof(requestLevel)}] [{requestLevel}] fell through the switch; defaulting!");

                                return new StaticDictionaryMediator(this._mediatorLogger);
                            }
                        }
                    }
                    default:
                    {
                        this._mediatorLogger.LogWarning($"The [{nameof(requestDataset)}] [{requestDataset}] fell through the switch; defaulting!");

                        return new StaticDictionaryMediator(this._mediatorLogger);
                    }
                }
            }
            else
            {
                this._mediatorLogger.LogWarning($"The [{nameof(datasetGroup)}] [{datasetGroup.Value}] could not be parsed; defaulting!");
                
                return new StaticDictionaryMediator(this._mediatorLogger);
            }
        }
        catch (Exception asYetUnknownError)
        {
            throw new CouldNotParseUrlPathException(asYetUnknownError.Message);
        }
    }
}
