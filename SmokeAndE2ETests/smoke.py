import requests
import sys
import os


HEROKU_TIMEOUT = 20


def smoke(url, arch_node):
    print("Testing: " + url)
    try:
        response = requests.get(url, timeout=HEROKU_TIMEOUT)
        if response.ok:
            print(response)
            print("SUCCESS: " + arch_node)
        else:
            print("On node " + arch_node + ":")
            raise requests.exceptions.RequestException()
    except requests.exceptions.RequestExceprion as e:
        print(e)
        sys.exit(-1)


if __name__ == "__main__":
    smoke(os.environ['ASPNET_PROTOCOL'] + '://' + os.environ['PSW_FEEDBACK_SERVICE_HOST'] + ('PSW_FEEDBACK_SERVICE_PORT' in os.environ.keys() ? (':' + os.environ['PSW_FEEDBACK_SERVICE_PORT']) : '') + '/feedbacks/feedback/published', "feedback microservice")
    smoke(os.environ['ASPNET_PROTOCOL'] + '://' + os.environ['PSW_HOSPITAL_SERVICE_HOST'] + ('PSW_HOSPITAL_SERVICE_PORT' in os.environ.keys() ? (':' + os.environ['PSW_HOSPITAL_SERVICE_PORT']) : '') + '/hospital/medication/', "hospital microservice")
    smoke(os.environ['ASPNET_PROTOCOL'] + '://' + os.environ['PSW_SCHEDULE_SERVICE_HOST'] + ('PSW_SCHEDULE_SERVICE_PORT' in os.environ.keys() ? (':' + os.environ['PSW_SCHEDULE_SERVICE_PORT']) : '') + '/schedule/examination/patient/1', "schedule microservice")
    smoke(os.environ['ASPNET_PROTOCOL'] + '://' + os.environ['PSW_USER_SERVICE_HOST'] + ('PSW_USER_SERVICE_PORT' in os.environ.keys() ? (':' + os.environ['PSW_USER_SERVICE_PORT']) : '') + '/user/country', "user microservice")
    smoke(os.environ['ASPNET_PROTOCOL'] + '://' + os.environ['PSW_API_GATEWAY_HOST'] + ('PSW_API_GATEWAY_PORT' in os.environ.keys() ? (':' + os.environ['PSW_API_GATEWAY_PORT']) : '') + '/api/user/city/by-country/1', "ocelot (asp.net) with database")
    smoke(os.environ['ASPNET_PROTOCOL'] + '://' + os.environ['PSW_WEB_FRONTEND_PUBLISH_HOST'] + ('PSW_WEB_FRONTEND_PUBLISH_PORT' in os.environ.keys() ? (':' + os.environ['PSW_WEB_FRONTEND_PUBLISH_PORT']) : '') + '/', "frontend")
