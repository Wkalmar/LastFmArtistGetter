open LastFmArtistGetter

[<EntryPoint>]
let main argv = 
    let result = getTopArtists
                |> getTopArtistNames
                |> removeAlreadyRecomendedArtists
                |> getUrlEncodedArtistNames 
                |> mapArtistNamesToArtistInfo getArtistInfo
                |> getArtistsShortInfo
                |> orderArtistsByListenersCount                     
    0
