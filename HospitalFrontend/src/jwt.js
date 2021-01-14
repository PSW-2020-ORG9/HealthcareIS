export function parseJwt(token) {
        var base64Url = token.split('.')[1]
        var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
        var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
        }).join(''))
    
        return JSON.parse(jsonPayload)
    }
import axios from 'axios'
import api from './constant/api.js'
export function setUser(app) {
        if(document.cookie) {
            let userToken = parseJwt(document.cookie.split('=')[1])
            if (userToken) {
                if (userToken.role == 'Patient') {
                    axios.get(api.patient + '/username')
                    .then(response => {
                        app.$store.commit('setUser', {
                            name: response.data.person.name,
                            surname: response.data.person.surname,
                            age: response.data.person.age,
                            role: 'Patient'
                        })
                    })
                } else if (userToken.role == 'Admin') {
                    app.$store.commit('setUser', {
                        name: 'Admin',
                        surname: '',
                        role: 'Admin'
                    })
                }
            }
            
        } else {
            app.$store.commit('setUser', null)
        }
}