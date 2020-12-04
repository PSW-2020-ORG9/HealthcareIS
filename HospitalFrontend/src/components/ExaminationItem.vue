<template>
    <div class="bg-light row rounded border border-info">
        <table class="table col-9 h-100">
            <thead>
                <tr class="table-info">
                    <th scope="col">
                        Start time
                    </th>
                    <th scope="col">
                        Room
                    </th>
                    <th scope="col">
                        Doctor
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="align-middle lead" rowspan="2">
                        {{getTime(examination.timeInterval.start)}}
                    </td>
                    <td>
                        {{examination.room.name}}
                    </td>
                    <td>
                        {{examination.doctor.person.name + " " + examination.doctor.person.surname}}
                    </td>
                </tr>
                <tr>
                    <td>
                        {{examination.room.department.description}}
                    </td>
                    <td>
                        {{examination.doctor.department.description}}
                    </td>
                </tr>
            </tbody>
        </table>
        <button class="btn btn-info col" v-if="isCancelable()">Cancel appointment</button>
        <div class="col text-danger lead font-weight-bold d-flex justify-content-center" v-else-if="examination.isCanceled">
            <h3 class="align-self-center">Canceled</h3>
        </div>
    </div>
</template>

<script>
export default {
    name: "ExaminationItem",
    props: {
        examination: Object
    },
    methods: {
        getDate: function(date) {
            let dateObj = new Date(date)
            return dateObj.getDate() + "." + (dateObj.getMonth() + 1) + "." + dateObj.getFullYear()
        },
        getTime: function(date) {
            let dateObj = new Date(date)
            let minutes = dateObj.getMinutes()
            return dateObj.getHours() + ":" + ((minutes < 10) ? ("0" + minutes) : minutes)
        },
        isCancelable: function () {
            if (this.examination.isCanceled) return false
            let dateObj = new Date(this.examination.timeInterval.start)
            let next48hours = new Date()
            next48hours.setDate(next48hours.getDate() + 2)
            if(dateObj.getTime() < next48hours.getTime()) return false
            return true
        }
    },
}
</script>

<style>

</style>