'use client'
import Link from "next/link"
import styles from "./Header.module.css"
import { useLogin } from "@/app/store";
import { usePathname } from "next/navigation";
import {CgProfile} from "react-icons/cg"
import { FaLongArrowAltRight } from "react-icons/fa";
import { useRouter } from "next/navigation";

function Header() {
  const router = useRouter()

  const pathName = usePathname();
  const isAuthenticated = useLogin(state => state.isLoggedIn);
  const curRole = useLogin(state => state.curRole)

  const isActive = (href) => pathName === href

  return (
    <header className = {styles["header_content"]}>
      <div className = {styles["header_content-wrapper"]}>
        <Link className = {styles["header_content-name"]} href = "/incidents">
          <div className= {styles["header_content-name-first"]}>
            Incidents
          </div>
          <div className = {styles["header_content-name-second"]}>
            <span className = {styles["special_character"]}>A</span>pplication
          </div>
        </Link>
        <nav className = {styles["links_wrapper"]}>
          {/* <Link href="/" className={isActive("/") ? styles["links_wrapper-active_link"]: styles["links_wrapper-link"]}>
              Main
          </Link> */}
          <Link href="/incidents"  className={isActive("/incidents") ? styles["links_wrapper-active_link"]: styles["links_wrapper-link"]}> 
              Incidents
          </Link>
          <Link href="/login"  className={isActive("/login") ? styles["links_wrapper-active_link"]: styles["links_wrapper-link"]}> 
              Login
          </Link>
        </nav>
      </div>
      <button className={styles["icon_wrapper"]} onClick={() =>{
          if(curRole != "")
          {
            router.push(`/profile/${curRole}`)
          }
          else{
            router.push("/login")
          }
      }}>
        <div className = {styles["profile_text"]}>Profile</div>
        <FaLongArrowAltRight className = {styles["arrow_icon_itself"]} />
        <CgProfile className = {styles["profile_icon_itself"]} />
      </button>
    </header>
  )
}
export default Header