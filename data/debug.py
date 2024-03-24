def error(msg):
    print('\033[91m' + f"[ERROR] {msg}" + '\033[0m')

def warn(msg):
    print('\033[93m' + f"[WARNING] {msg}" + '\033[0m')

def info(msg):
    print('\033[96m' + f"[INFO] {msg}" + '\033[0m')

def log(msg):
    print(f"[LOG] {msg}")