//Confirm delete
const deleteDivs = document.querySelectorAll(".deleteDiv a");
deleteDivs.forEach(element => {
    element.addEventListener("click", (event) =>{     
        event.preventDefault();    
        const elementId = event.target.getAttribute("data-id");
        

        confirmDelete(elementId)
        
    })
});


const confirmDeleteDivs = document.querySelectorAll(".confirmDeleteDiv a");
confirmDeleteDivs.forEach(element => {
    element.addEventListener("click", (event) =>{  
        event.preventDefault();       
        const userId = event.target.getAttribute("data-id");        

        confirmDelete(userId)
        
    })
});


function confirmDelete(uniqueId) {    
    let deleteDiv = document.getElementById('deleteDiv_' + uniqueId)   
    let confirmDeleteDiv = document.getElementById('confirmDeleteDiv_' + uniqueId)    
    
    deleteDiv.classList.toggle("hide");
    confirmDeleteDiv.classList.toggle("hide");   
}


//Change color if thumbsUp
const checkBoxes = document.querySelectorAll('.thumbsUpInput') 
const checkBoxesLabel = document.querySelectorAll('.thumbsUpLabel')
    for (let i = 0; i < checkBoxes.length; i++) {
        const element = checkBoxes[i];
        if (element.checked == true) {            
            checkBoxesLabel[i].children[0].className = "isThumbsUp";
        }
    }




    //EventLister på thumbsUpLabel
    
    // $.ajax({
    //     type: "post",
    //     url: "/User/Activities/Vote",
    //     contentType: "application/json",
    //     dataType: "json",
    //     success: function () {            
    //         console.log("Succes")
    //     },
    //     failure: function () {
    //         console.log("Error");
    //     }
    // });

