namespace LastFmArtistGetter

[<AutoOpen>]
module BL =

    open FSharp.Data
    open System
    open System.Web

    [<Literal>]
    let userName = "<login here>"

    [<Literal>]
    let apiKey = "<api key here>"

    [<Literal>]
    let baseUrl = "http://ws.audioscrobbler.com"

    [<Literal>]
    let getTopArtistsPattern = "{0}/2.0/?method=user.gettopartists&user={1}&api_key={2}&period=12month&format=json"

    [<Literal>]
    let getArtistInfoPattern = "{0}/2.0/?method=artist.getinfo&artist={1}&api_key={2}&format=json"

    let alreadyRecomendedArtists = [|
        "Alice in Chains"
        "Serj Tankian"
        "Porcupine Tree"
        "The Mars Volta" 
        "Frank Zappa" 
        "Eddie Vedder" 
        "Alter Bridge" 
        "Opeth" 
        "Max Richter" 
        "Parkway Drive" 
        "Nils Frahm" 
        "Arvo Pärt" 
        "Buckethead" 
        "Riverside" 
        "Agalloch" 
        "Mahavishnu Orchestra" 
        "Jefferson Airplane" 
        "King Crimson" 
        "Hammock" 
        "坂本龍一" 
    |]

    let getTopArtists () = 
        try
            let path = String.Format(getTopArtistsPattern, baseUrl, userName, apiKey)
            let data = Http.Request(path)
            match data.Body with
            | Text text -> Ok(TopArtists.Parse(text).Topartists.Artist)
            | _ -> Error "getTopArtists. Unexpected format of reponse message"
        with
        | ex -> Error ex.Message

    let getTopArtistNames (artists: TopArtists.Artist[])  =
        let names = 
            artists
                |> Array.map (fun i -> i.Name)
        Ok(names)

    let artistWasNotRecommended artist =
        (Array.contains artist alreadyRecomendedArtists) = false

    let removeAlreadyRecomendedArtists artists = 
        let filtered = 
            artists
                |> Array.filter (fun i -> artistWasNotRecommended i)
        Ok(filtered)

    let getUrlEncodedArtistNames (artists: string[]) =
        let encoded = 
            artists
                |> Array.map (fun i -> HttpUtility.UrlEncode i)
        Ok(encoded)

    let mapArtistNamesToArtistInfo getArtistInfoFn artists = 
        try
            let artistInfos = 
                artists
                    |> Array.map (fun i -> getArtistInfoFn i) 
            Ok(artistInfos)
        with
        | ex -> Error ex.Message

    exception GetArtistInfoFormatException of string
    
    let getArtistInfo artistName = 
        let path = String.Format(getArtistInfoPattern, baseUrl, artistName, apiKey)
        let data = Http.Request(path)
        match data.Body with
        | Text text -> ArtistInfo.Parse(text)
        | _ -> raise(GetArtistInfoFormatException("getArtistInfo. Unexpected format of reponse message"))

    let mapArtistInfoToArtistShortInfo (artistInfo: ArtistInfo.Root) = 
        try
            let res = { name = artistInfo.Artist.Name; 
                        listeners = artistInfo.Artist.Stats.Listeners }
            res
        with
        | _ -> {name = "Not Found"; listeners = 0}

    let getArtistsShortInfo artists =
        let shortInfos = 
            artists
                |> Array.map (fun i -> mapArtistInfoToArtistShortInfo i)
        Ok(shortInfos)

    let orderArtistsByListenersCount artists =
        let ordered = 
                artists
                    |> Array.sortBy (fun i -> -i.listeners)
        Ok(ordered)
