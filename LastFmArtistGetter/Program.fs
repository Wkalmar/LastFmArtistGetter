open LastFmArtistGetter

let pipeline = 
    getTopArtists
        >=> getTopArtistNames
        >=> removeAlreadyRecomendedArtists
        >=> getUrlEncodedArtistNames 
        >=> mapArtistNamesToArtistInfo getArtistInfo
        >=> getArtistsShortInfo
        >=> orderArtistsByListenersCount

[<EntryPoint>]
let main argv = 
    let result = pipeline()
    0
