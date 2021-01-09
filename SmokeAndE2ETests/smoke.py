import requests


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
    smoke('https://psw-eu-feedback.herokuapp.com/feedbacks/feedback/published', "feedback microservice")
    smoke('https://psw-eu-hospital.herokuapp.com/hospital/medication/', "hospital microservice")
    smoke('https://psw-eu-schedule.herokuapp.com/schedule/examination/patient/1', "schedule microservice")
    smoke('https://psw-eu-user.herokuapp.com/user/country', "user microservice")
    smoke('https://psw-eu-ocelot.herokuapp.com/api/user/city/by-country/1', "ocelot (asp.net) with database")
    smoke('https://eu-healthcareis.herokuapp.com/', "frontend")
