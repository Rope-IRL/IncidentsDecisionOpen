'use client'
import {useState, useEffect} from "react"
import { useRouter } from "next/navigation";
import styles from "./employee_profile.module.css"
import { AiOutlineUser } from "react-icons/ai";
import { BsFillPersonLinesFill } from "react-icons/bs";
import { BsFillPersonVcardFill } from "react-icons/bs";
import { HiDevicePhoneMobile } from "react-icons/hi2";

function EmployeeProfile() {
  
  const router = useRouter();

  const [name, setName] = useState("");
  const [surname, setSurname] = useState("");
  const [telephone, setTelephone] = useState("");
  const [gender, setGender] = useState("");

  
  const assignData = async () => {
    let resName = "";
    let resSurname = "";
    let resTelephone = "";
    let resGender = "";
    try{
        const response = await fetch(`${process.env.NEXT_PUBLIC_BASE_URL}/employee/info`, {
          method:"GET",
          credentials:"include"
        })
    

        if(response.ok){
          const data = await response.json()
          resName = data.name;
          resSurname = data.surname;
          resTelephone = data.telephone;
          resGender = data.gender;
        }
        
        else{
          console.log(response);
          router.push("/login");
        }
      }
      
      catch(error){
        // setError(error);
        console.log(error);
        router.push("/login");
      }
      
      finally{  
        setName(resName);
        setSurname(resSurname);
        setTelephone(resTelephone);
        setGender(resGender);
      }
  }

  useEffect(() => {
    assignData();
  }, [])

  return (
      <div className = {styles["profile_wrapper"]}>
        <div className = {styles["profile_wrapper-header"]}>
          This is <span className = {styles["special_character"]}>Y</span>our profile
        </div>
        <div className = {styles["profile_wrapper-info"]}>
          <div className = {styles["profile_wrapper-info-property"]}>
            <div className = {styles["property_name"]}>
              <div>
                Name
              </div>
              <BsFillPersonLinesFill />
            </div>
            <div className = {styles["property_content"]}>
              {name}
            </div>
          </div>
          <div className = {styles["profile_wrapper-info-property"]}>
            <div className = {styles["property_name"]}>
              <div>
                Surname
              </div>
              <BsFillPersonVcardFill />
            </div>
            <div className = {styles["property_content"]}>
              {surname}
            </div>
          </div>
          <div className = {styles["profile_wrapper-info-property"]}>
            <div className = {styles["property_name"]}>
              <div>
                Telephone
              </div>
              <HiDevicePhoneMobile />
            </div>
            <div className = {styles["property_content"]}>
              {telephone}
            </div>
          </div>
          <div className = {styles["profile_wrapper-info-property"]}>
            <div className = {styles["property_name"]}>
              <div>
                Gender
              </div>
              <AiOutlineUser />
            </div>
            <div className = {styles["property_content"]}>
              {gender}
            </div>
          </div>
        </div>
      </div>
  )
}
export default EmployeeProfile