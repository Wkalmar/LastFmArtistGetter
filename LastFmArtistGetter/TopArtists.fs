namespace LastFmArtistGetter

[<AutoOpen>]
module TopArtists  =

    open FSharp.Data

    let [<Literal>] TopArtistsSample = """{  
    "topartists":{  
        "artist":[  
            {  
            "name":"Porcupine Tree",
            "playcount":"48",
            "mbid":"169c4c28-858e-497b-81a4-8bc15e0026ea",
            "url":"https://www.last.fm/music/Porcupine+Tree",
            "streamable":"0",
            "image":[  
                {  
                    "#text":"https://lastfm-img2.akamaized.net/i/u/34s/e317676d1d004ae79b14afb03ddcf535.png",
                    "size":"small"
                },
                {  
                    "#text":"https://lastfm-img2.akamaized.net/i/u/64s/e317676d1d004ae79b14afb03ddcf535.png",
                    "size":"medium"
                },
                {  
                    "#text":"https://lastfm-img2.akamaized.net/i/u/174s/e317676d1d004ae79b14afb03ddcf535.png",
                    "size":"large"
                },
                {  
                    "#text":"https://lastfm-img2.akamaized.net/i/u/300x300/e317676d1d004ae79b14afb03ddcf535.png",
                    "size":"extralarge"
                },
                {  
                    "#text":"https://lastfm-img2.akamaized.net/i/u/e317676d1d004ae79b14afb03ddcf535.png",
                    "size":"mega"
                }
            ],
            "@attr":{  
                "rank":"1"
            }
            },
            {  
            "name":"Muslimgauze",
            "playcount":"45",
            "mbid":"06fc1189-d7cd-4344-b09a-51cd82cfefe5",
            "url":"https://www.last.fm/music/Muslimgauze",
            "streamable":"0",
            "image":[  
                {  
                    "#text":"https://lastfm-img2.akamaized.net/i/u/34s/2f52b3bb718b47dcc73ae53ab329685f.png",
                    "size":"small"
                },
                {  
                    "#text":"https://lastfm-img2.akamaized.net/i/u/64s/2f52b3bb718b47dcc73ae53ab329685f.png",
                    "size":"medium"
                },
                {  
                    "#text":"https://lastfm-img2.akamaized.net/i/u/174s/2f52b3bb718b47dcc73ae53ab329685f.png",
                    "size":"large"
                },
                {  
                    "#text":"https://lastfm-img2.akamaized.net/i/u/300x300/2f52b3bb718b47dcc73ae53ab329685f.png",
                    "size":"extralarge"
                },
                {  
                    "#text":"https://lastfm-img2.akamaized.net/i/u/2f52b3bb718b47dcc73ae53ab329685f.png",
                    "size":"mega"
                }
            ],
            "@attr":{  
                "rank":"2"
            }
            }
        ],
        "@attr":{  
            "user":"Morbid_soul",
            "page":"1",
            "perPage":"2",
            "totalPages":"165",
            "total":"330"
        }
    }
    }"""
    type TopArtists = JsonProvider<TopArtistsSample>
