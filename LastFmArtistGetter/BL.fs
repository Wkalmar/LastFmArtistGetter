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
    |]

    let getTopArtists = 
        let path = String.Format(getTopArtistsPattern, baseUrl, userName, apiKey)
        let data = Http.Request(path)
        match data.Body with
        | Text text -> TopArtists.Parse(text).Topartists.Artist 
        | _ -> null

    let getTopArtistNames (artists: TopArtists.Artist[])  =
        artists
            |> Array.map (fun i -> i.Name) 

    let artistWasNotRecommended artist =
        (Array.contains artist alreadyRecomendedArtists) = false

    let removeAlreadyRecomendedArtists artists = 
        artists
            |> Array.filter (fun i -> artistWasNotRecommended i)

    let getUrlEncodedArtistNames (artists: string[]) =
        artists
            |> Array.map (fun i -> HttpUtility.UrlEncode i)

    let mapArtistNamesToArtistInfo getArtistInfoFn artists = 
        artists
            |> Array.map (fun i -> getArtistInfoFn i) 

    let getArtistInfo artistName = 
        let path = String.Format(getArtistInfoPattern, baseUrl, artistName, apiKey)
        let data = Http.Request(path)
        match data.Body with
        | Text text -> ArtistInfo.Parse(text)
        | _ -> ArtistInfo.Parse(null)

    let mapArtistInfoToArtistShortInfo (artistInfo: ArtistInfo.Root) = 
        let res = { name = artistInfo.Artist.Name; 
                    listeners = artistInfo.Artist.Stats.Listeners }
        res

    let getArtistsShortInfo artists =
        artists
            |> Array.map (fun i -> mapArtistInfoToArtistShortInfo i)

    let orderArtistsByListenersCount artists =
        artists
            |> Array.sortBy (fun i -> -i.listeners)
