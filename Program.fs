// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open System

let sortStringAsKey (listOfWords : string array) = 
    let getKey (str : string) = 
        str.ToCharArray() 
        |> Array.sort 
        |> String

    listOfWords 
    |> Array.groupBy getKey 
    |> Array.filter (fun (_, originalWord) -> originalWord.Length > 1)

     
[<EntryPoint>]
let main argv = 
    let filename = "words.txt"
    let listOfWords = System.IO.File.ReadAllLines(filename)
    let listOfAnagrams = sortStringAsKey listOfWords
    
    // Prints every single anagram combination
    listOfAnagrams |> Array.iter  (fun (_ , anagramList) ->  anagramList |> Array.iter (fun str -> printfn "%s" str); printfn "")

    // Gets the LONGEST anagram
    let longestAnagrams = listOfAnagrams 
                        |> Array.filter (fun (sortedWord, _) -> sortedWord.Length >= (listOfAnagrams 
                                                                                        |> Array.maxBy (fun (sortedWord, _)-> sortedWord.Length) 
                                                                                        |> fst 
                                                                                        |> String.length))

    // Gets the set that has the MOST anagrams
    let mostAnagrams = listOfAnagrams
                        |> Array.filter (fun (_, originalWords) -> originalWords.Length >= (listOfAnagrams 
                                                                                            |> Array.maxBy (fun (_, originalWords) -> originalWords.Length) 
                                                                                            |> snd 
                                                                                            |> Array.length))

    // Prints the longest and most anagrams
    longestAnagrams |> Array.iter  (fun (_ , anagramList) ->  anagramList |> Array.iter (fun str -> printfn "%s" str); printfn "")
    mostAnagrams |> Array.iter  (fun (_ , anagramList) ->  anagramList |> Array.iter (fun str -> printfn "%s" str); printfn "")
  

    0 // return an integer exit code
