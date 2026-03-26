import time
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.chrome.service import Service
from webdriver_manager.chrome import ChromeDriverManager

# Basic Config
# Note: Check your Visual Studio port number (localhost:XXXX) and update if different
BASE_URL = "https://localhost:7257" 
LOGIN_URL = f"{BASE_URL}/Account/Login"
TEST_USER = "admin@admsystems.net"
TEST_PASS = "password123"

def run_login_test():
    # Setup Chrome Driver
    options = webdriver.ChromeOptions()
    options.add_argument("--start-maximized")
    driver = webdriver.Chrome(service=Service(ChromeDriverManager().install()), options=options)
    
    try:
        print(f"[*] Navigating to {LOGIN_URL}...")
        driver.get(LOGIN_URL)
        time.sleep(2)  # Wait for page load
        
        # Locate elements and perform login
        print("[*] Entering credentials...")
        driver.find_element(By.ID, "Email").send_keys(TEST_USER)
        driver.find_element(By.ID, "Password").send_keys(TEST_PASS)
        
        # Click login button
        print("[*] Submitting login form...")
        driver.find_element(By.CSS_SELECTOR, "button[type='submit']").click()
        
        # Validation
        time.sleep(3)
        current_url = driver.current_url
        
        if "/Item" in current_url:
            print("[+] SUCCESS: Login verified. Redirected to Item Inventory.")
        else:
            print("[-] FAILED: Login did not redirect to the expected dashboard.")
            print(f"[-] Current URL is: {current_url}")
            
    except Exception as e:
        print(f"[!] ERROR: An exception occurred during the test: {str(e)}")
        
    finally:
        print("[*] Closing browser...")
        driver.quit()

if __name__ == "__main__":
    run_login_test()