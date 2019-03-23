open LastFmArtistGetter

let pipeline = 
    getTopArtists()
        |> Result.bind getTopArtistNames
        |> Result.bind removeAlreadyRecomendedArtists
        |> Result.bind getUrlEncodedArtistNames 
        |> Result.bind (mapArtistNamesToArtistInfo getArtistInfo)
        |> Result.bind getArtistsShortInfo
        |> Result.bind orderArtistsByListenersCount

[<EntryPoint>]
let main argv = 
    let result = pipeline
    0
