let server='http://localhost:5000'
let docSearch = server + '/docsearch'

export default{
    feedback: server + '/feedback',
    patient: server + '/patient',
    docSearchPrescriptionSimple: docSearch + '/prescription/simple/',
    docSearchExaminationSimple: docSearch + '/examination/simple'
}