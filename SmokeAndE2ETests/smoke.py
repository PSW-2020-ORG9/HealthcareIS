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
    smoke('https://psw-backend.herokuapp.com/city/by-country/1', "backend (asp.net) with database")
    smoke('https://psw-front.herokuapp.com/', "frontend")