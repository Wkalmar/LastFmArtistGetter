module ArrayHelpers

let rec withoutElementRec element list skipped = 
    match list with
    | [] -> None
    | head::tail when element = head -> 
        let skipped' = List.rev skipped
        Some (skipped' @ tail)
    | head::tail  -> 
        let skipped' = head :: skipped
        withoutElementRec element tail skipped' 

let withoutElement x list = 
    withoutElementRec x list [] 

let rec isPermutationOf list1 list2 = 
    match list1 with
    | [] -> List.isEmpty list2
    | h1::t1 -> 
        match withoutElement h1 list2 with
        | None -> false
        | Some t2 -> 
            isPermutationOf t1 t2