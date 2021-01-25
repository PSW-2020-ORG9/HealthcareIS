import api from '../constant/api.js'
import axios from 'axios'

export function publishSchedulingEvent(data){
    axios.post(api.schedulingEventUrl,data).then(response=>{
        return response
    })
}
