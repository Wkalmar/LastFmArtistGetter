namespace LastFmArtistGetter.Tests

module BLTests =

    open Xunit
    open LastFmArtistGetter

    [<Fact>]
    let removeAlreadyRecomendedArtists_returns_expected_result() =         
        let result = removeAlreadyRecomendedArtists [|"Serj Tankian"; "Richard Chartier"; "Alter Bridge"; "Genocide Organ"|]
        Assert.Equal(result.[0], "Richard Chartier")
        Assert.Equal(result.[1], "Genocide Organ")

    [<Fact>]
    let getUrlEncodedArtistNames_returns_expected_result() =
        let result = getUrlEncodedArtistNames [|"Bohren & Der Club Of Gore"; "Цукор Біла Смерть"|]
        Assert.Equal(result.[0], "Bohren+%26+Der+Club+Of+Gore")
        Assert.Equal(result.[1], "%d0%a6%d1%83%d0%ba%d0%be%d1%80+%d0%91%d1%96%d0%bb%d0%b0+%d0%a1%d0%bc%d0%b5%d1%80%d1%82%d1%8c")

    let getArtistInfoStub input = 
        match input with
        | "Nokturanl Mortum" -> 1
        | "Krobak" -> 2
        | _ -> 3

    [<Fact>]
    let mapArtistNamesToArtistInfo_returns_expected_result() =
        let result = mapArtistNamesToArtistInfo getArtistInfoStub [| "Nokturanl Mortum"; "Heinali"; "Krobak"|]
        Assert.Equal(result.[0], 1)
        Assert.Equal(result.[1], 3)
        Assert.Equal(result.[2], 2)

    [<Fact>]
    let orderArtistsByListenersCount_returns_expected_result() =
        let Satie = {name = "Erik Satie"; listeners = 750000}
        let Chopin = {name ="Frederic Chopin"; listeners = 1200000}
        let Barber = {name = "Samuel Barber"; listeners = 371000}
        let artists = [|Satie; Chopin; Barber|]
        let result = orderArtistsByListenersCount artists
        Assert.Equal(result.[0], Chopin)
        Assert.Equal(result.[1], Satie)
        Assert.Equal(result.[2], Barber)