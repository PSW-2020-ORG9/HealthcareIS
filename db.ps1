docker container stop dev-db
docker container rm dev-db
docker container stop test-db
docker container rm test-db
docker container stop master-db
docker container rm master-db
$param = $Args[0]
if ($param -eq 'dev' -or $param -eq 'test' -or $param -eq 'master') {
    $project = ''
    if($param -eq 'dev'){
        $project = 'HealthcareBase'
    } else {
        $project = 'HospitalWebAppIntegrationTests'
    }
    if($param -ne 'master'){
        dotnet ef dbcontext script -o $($project + '/dbscheme.sql') --project $project
        Copy-Item DevOps/docker/DBDockerfile $($project + '/Dockerfile')
        Set-Location $project
        docker build . -t $($param + '-db-img:latest')
        Set-Location ..
        docker run --name $($param + '-db') -d -e $('MYSQL_ROOT_PASSWORD=' + $env:DB_PSW_PASSWORD) -e $('MYSQL_DATABASE=' + $env:DB_PSW_DATABASE) -p $($env:DB_PSW_PORT + ':3306') $($param + '-db-img:latest')
        Remove-Item $($project + '/Dockerfile')
        Remove-Item $($project + '/dbscheme.sql')
    } else {
        docker run --name $($param + '-db') -d -e $('MYSQL_ROOT_PASSWORD=' + $env:DB_PSW_PASSWORD) -e $('MYSQL_DATABASE=' + $env:DB_PSW_DATABASE) -p $($env:DB_PSW_PORT + ':3306') fmaster/pswdb:latest
    }
} else {
    Write-Output "wrong parameters"
}
