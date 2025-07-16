'use client'
import Link from "next/link"
import styles from "./login.module.css"

function Login() {
  return (
    <div className = {styles["logins_info"]}>
        <div className = {styles["logins_info_header"]}>
            Login options
        </div>
        <div className = {styles["logins_wrapper"]}>
            <Link href="/login/employee" className = {styles["login_link-employee"]}>
                I am <span className = {styles["special_character"]}>E</span>mployee
            </Link>
            <Link href="/login/tech_support" className = {styles["login_link-tech_support"]}>
                I am from <span className = {styles["special_character"]}>T</span>ech Support
            </Link>
        </div>
    </div>
  )
}
export default Login