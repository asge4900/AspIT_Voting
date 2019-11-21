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