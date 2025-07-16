'use client'
import styles from "./employee_login.module.css"
import { useLogin } from "@/app/store";
import { useRouter } from "next/navigation";
import { useState } from "react";

function EmployeeLogin(){
    const router = useRouter()
    const [login, setLogin] = useState("");    
    const [password, setPassword] = useState("");

    const authenticate = useLogin(state => state.logInEmployee);

    return (
        <div className = {styles["credentials_wrapper"]}>
            <div className = {styles["credentials_wrapper-header"]}>
                <span className = {styles["special_character"]}>E</span>mployee Login
            </div>
            <form className = {styles["form_wrapper"]}>
                <input 
                    placeholder="Login"
                    className = {styles["input_login"]}
                    type = "email"
                    value={login}
                    onChange = {(e) => {
                            setLogin(e.target.value)
                        }
                    }
                />

                <input 
                    placeholder="Password"
                    className = {styles["input_password"]}
                    type = "password"
                    value={password}
                    onChange = {(e) => {
                            setPassword(e.target.value)
                        }
                    }
                />

                <button
                    className = {styles["login_button"]}
                    onClick = {(e) => {
                        e.preventDefault()
                        authenticate(login, password);
                        router.push("/")
                    }}
                >Login</button>
            </form>
        </div>
    )
}

export default EmployeeLogin;