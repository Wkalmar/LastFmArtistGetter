namespace LastFmArtistGetter.Tests

module BLTests =

    open Xunit
    open LastFmArtistGetter

    [<Fact>]
    let removeAlreadyRecomendedArtists_returns_expected_result() =         
        let result = removeAlreadyRecomendedArtists [|"Serj Tankian"; "Richard Chartier"; "Alter Bridge"; "Genocide Organ"|]
        match result with
        | Success s ->
            Assert.Equal(s.[0], "Richard Chartier")
            Assert.Equal(s.[1], "Genocide Organ")
        | Failure _ -> Assert.True(false)

    [<Fact>]
    let getUrlEncodedArtistNames_returns_expected_result() =
        let result = getUrlEncodedArtistNames [|"Bohren & Der Club Of Gore"; "Цукор Біла Смерть"|]
        match result with
        | Success s ->
            Assert.Equal(s.[0], "Bohren+%26+Der+Club+Of+Gore")
            Assert.Equal(s.[1], "%d0%a6%d1%83%d0%ba%d0%be%d1%80+%d0%91%d1%96%d0%bb%d0%b0+%d0%a1%d0%bc%d0%b5%d1%80%d1%82%d1%8c")
        | Failure _ -> Assert.True(false)

    let getArtistInfoStub input = 
        match input with
        | "Nokturanl Mortum" -> 1
        | "Krobak" -> 2
        | _ -> 3

    [<Fact>]
    let mapArtistNamesToArtistInfo_returns_expected_result() =
        let result = mapArtistNamesToArtistInfo getArtistInfoStub [| "Nokturanl Mortum"; "Heinali"; "Krobak"|]
        match result with 
        | Success s ->
            Assert.Equal(s.[0], 1)
            Assert.Equal(s.[1], 3)
            Assert.Equal(s.[2], 2)
        | Failure _ -> Assert.True(false)

    [<Fact>]
    let orderArtistsByListenersCount_returns_expected_result() =
        let Satie = {name = "Erik Satie"; listeners = 750000}
        let Chopin = {name ="Frederic Chopin"; listeners = 1200000}
        let Barber = {name = "Samuel Barber"; listeners = 371000}
        let artists = [|Satie; Chopin; Barber|]
        let result = orderArtistsByListenersCount artists
        match result with
        | Success s -> 
            Assert.Equal(s.[0], Chopin)
            Assert.Equal(s.[1], Satie)
            Assert.Equal(s.[2], Barber)
        | Failure _ -> Assert.True(false)