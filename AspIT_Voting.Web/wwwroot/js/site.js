//EventListener for slet
const deleteDivs = document.querySelectorAll(".deleteDiv a");
deleteDivs.forEach(element => {
    element.addEventListener("click", (event) =>{         
        const elementId = event.target.getAttribute("data-id");
        

        confirmDelete(elementId)
        
    })
});

//EventListener for nej
const confirmDeleteDivs = document.querySelectorAll(".confirmDeleteDiv a");
confirmDeleteDivs.forEach(element => {
    element.addEventListener("click", (event) =>{         
        const userId = event.target.getAttribute("data-id");        

        confirmDelete(userId)
        
    })
});

//Hide or show confirmDelete
function confirmDelete(uniqueId) {    
    let deleteDiv = document.getElementById('deleteDiv_' + uniqueId)   
    let confirmDeleteDiv = document.getElementById('confirmDeleteDiv_' + uniqueId)    
    
    deleteDiv.classList.toggle("hide");
    confirmDeleteDiv.classList.toggle("hide");   
}