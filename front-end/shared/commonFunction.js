//  async function HttpPost(url, data) {
//     // fetch('http://localhost:4000/api/persons/getPersonById', {
//     //     method: 'POST', // or 'PUT'
//     //     headers: {
//     //         'Content-Type': 'application/json',
//     //     },
//     //     body: JSON.stringify(data),
//     // })
//     //     .then(response => response.json())
//     //     .then(data => {
//     //         console.log('Success:', data);
//     //     })
//     //     .catch((error) => {
//     //         console.error('Error:', error);
//     //     });
//     return    fetch(`http://localhost:4000${url}`, {
//             method: 'POST', // or 'PUT'
//             headers: {
//                 'Content-Type': 'application/json',
//             },
//             body: JSON.stringify(data),
//         })
// } 