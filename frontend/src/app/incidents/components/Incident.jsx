'use client'
import styles from "./Incident.module.css"
import { SlNote } from "react-icons/sl";
import { useRouter } from "next/navigation";
import { useIncident } from "@/app/store";

function Incident({id = 0, name = "", description = "", createdAt = "", type =""}) {
  const router = useRouter()

  const setCurIncident = useIncident(state => state.setCurEditableIncident)

  const date = new Date(createdAt);
  const day = date.getDate();
  const month = date.getMonth() + 1;
  const year = date.getFullYear();
  const hour = date.getHours();
  const minutes = date.getMinutes();
  const minutes_updated = minutes%10 == minutes ? `0${minutes}`: minutes.toString();

  return (
    <div className = {styles["incident_wrapper"]}>
        <div className = {styles["incident_wrapper-headers"]}>
          <div>Name</div>
          <div>Description</div>
          <div>Date</div>
          <div>Time</div>
        </div>
        <div className = {styles["incident_wrapper-info"]}>
          <div className = {styles["incident_name"]}>
              {name.length > 30 ? name.slice(0, 30) + "..." : name}
          </div>
          <div className = {styles["incident_description"]}>
              {description.length > 30 ? description.slice(0, 30) + "...": description}
          </div>
          <div>
            <div>
              {day}/{month}/{year} (d/m/y)
            </div>
            <div>
              {hour}:{minutes_updated} (UTC+3)
            </div>
          </div>
        </div>
        <div className = {styles["motions_wrapper"]}>
          <button className = {styles["edit_btn_wrapper"]}
            onClick = {() =>{
              setCurIncident(id, day, month, year, hour, minutes, name, description, type)
              router.push("/incidents/edit")
            }}
          >
            <SlNote />
            <div>
              Edit
            </div>
          </button>
        </div>
    </div>
  )
}
export default Incident