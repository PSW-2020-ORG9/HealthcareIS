import api from '../constant/api.js'
import axios from 'axios'

export function publishSchedulingEvent(data){
    console.log(data)
    axios.post(api.schedulingEventUrl,data).then(response=>{
        return response
    })
}