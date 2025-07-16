'use client'
import { useIncident, useLogin } from "@/app/store";
import {useState} from "react"
import styles from "./incidents_edit.module.css"
import { useRouter } from "next/navigation";

function EditIncident(){
    const router = useRouter()
    const getCurIncident = useIncident(state => state.getCurEditableIncident)
    const {id, day, month, year, hour, minutes, name, description, type} = getCurIncident();

    const [error, setError] = useState("")
    const [curDay, setCurDay] = useState(day)
    const [curMonth, setCurMonth] = useState(month)
    const [curYear, setCurYear] = useState(year)
    const [curHour, setCurHour] = useState(hour)
    const [curMinutes, setCurMinutes] = useState(minutes)
    const [curName, setCurName] = useState(name)
    const [curDescription, setCurDescription] = useState(description)
   
    async function editIncident()
    {
        try{
            const urlType = type == "notResolved" ? "notresolvedincident" : "resolvedincident"
            const res = await fetch(`${process.env.NEXT_PUBLIC_BASE_URL}/${urlType}`, {
                headers:{
                    "Content-Type":"application/json"
                },
                method:"POST",
                credentials:"include",
                body:JSON.stringify({
                    id:id,
                    day:curDay,
                    month:curMonth,
                    year:curYear,
                    hour:curHour,
                    minutes:curMinutes,
                    name:curName,
                    description:curDescription
                })
            })
    
            if(res.ok == false)
            {
                if(res.status === 403)
                {
                    setError("You don't have permission to edit incidents, please login as current tech stuff")
                }
                else{
                    setError(await res.text())
                }
            }
            else{
                router.push("/incidents")
            }
        }
        catch(error)
        {
            setError(error)
        }
    
    }

    async function moveIncident()
    {
        if(type == "notResolved")
        {

            try{
                const res = await fetch(`${process.env.NEXT_PUBLIC_BASE_URL}/resolvedincident`, {
                    headers:{
                        "Content-Type":"application/json"
                    },
                    method:"PUT",
                    credentials:"include",
                    body:JSON.stringify({
                        day:curDay,
                        month:curMonth,
                        year:curYear,
                        hour:curHour,
                        minutes:curMinutes,
                        name:curName,
                        description:curDescription
                    })
                })
                
                if(res.ok == false){
                    if(res.status == 403)
                    {
                        setError("You don't have permission to edit incidents, please login as current tech stuff")
                    }
                    else{
                        setError(await res.text())
                    }
                }
                else{
                    
                    const res = await fetch(`${process.env.NEXT_PUBLIC_BASE_URL}/notresolvedincident/${id}`,{
                        method:"DELETE",
                        credentials:"include"
                    })
                    if(res.ok == false)
                        {
                            if(res.status === 403){
                                setError("You don't have permission to edit incidents, please login as current tech stuff")
                            }
                            else{
                                    setError(await res.text())
                                }
                            }
                            else{
                                router.push("/incidents")
                            }
                }
            }
            catch(error){
                setError(error)
            }
        }
        else{
            setError("You can't move this incident")
        }
    }

    async function deleteIncident()
    {
        try{
            const urlType = type == "notResolved" ? "notresolvedincident" : "resolvedincident"
            const res = await fetch(`${process.env.NEXT_PUBLIC_BASE_URL}/${urlType}/${id}`,{
                method:"DELETE",
                credentials:"include"
            })

            if(res.ok == false)
            {
                if(res.status == 403)
                {
                    setError("You don't have permission to edit incidents, please login as current tech stuff")
                }
                else{
                    setError(await res.text())
                }
            }
            else{
                router.push("/incidents")
            }

        }
        catch(error){
            setError(error)
        }
    }

    return (
        <div className = {styles["incident_wrapper"]}>
            <div className = {styles["incident_wrapper-header"]}>
                Incident <span className = {styles["special_character"]}>E</span>diting page
            </div>
            <div className = {styles["error_wrapper"]}>
                {error}
            </div>
            <form className = {styles["incident_fields"]}>
                <input 
                    className = {styles["incident_field"]}
                    value = {curName}
                    type="text"
                    placeholder="Name"
                    onChange={(e) => {
                        setCurName(e.target.value)
                    }}
                />
                <input 
                    className = {styles["incident_field"]}
                    value = {curDescription}
                    type="text" 
                    placeholder="Description"
                    onChange={(e) => {
                        setCurDescription(e.target.value)
                    }}
                />
                <input 
                    className = {styles["incident_field"]}
                    value={curDay}
                    type="text" 
                    placeholder="Day"
                    onChange = {(e) => {
                        setCurDay(e.target.value)
                    }}
                />
                <input 
                    className = {styles["incident_field"]}
                    value={curMonth}
                    type="text"
                    placeholder = "Month"
                    onChange = {(e) => {
                        setCurMonth(e.target.value)
                    }}
                />
                <input 
                    className = {styles["incident_field"]}
                    value={curYear}
                    type="text" 
                    placeholder="Year"
                    onChange = {(e) => {
                        setCurYear(e.target.value)
                    }}
                />
                <input 
                    className = {styles["incident_field"]}
                    value={curHour}
                    type="text"
                    placeholder="Hour"
                    onChange = {(e) => {
                        setCurHour(e.target.value)
                    }}
                />
                <input 
                    className = {styles["incident_field"]}
                    value={curMinutes}
                    type="text" 
                    placeholder="Minutes"
                    onChange={(e) => {
                        setCurMinutes(e.target.value)
                    }}
                />
                <button className = {styles["save_button"]}
                    onClick={(e) => {
                        e.preventDefault()
                        editIncident()
                    }}
                > 
                    <span className = {styles["special_character"]}>S</span>ave 
                </button>
            </form>
            <div className = {styles["actions_btn_wrapper"]}>
                <button
                    className = {styles["resolved_btn"]}
                    onClick={(e) => {
                        e.preventDefault()
                        moveIncident()
                    }}
                >
                    <span className = {styles["special_character"]}>M</span>ove to Resolved</button>
                <button
                    className = {styles["delete_btn"]}
                    onClick={(e) => {
                        e.preventDefault()
                        deleteIncident()
                    }}
                ><span className = {styles["special_character"]}>D</span>elete Incident</button>
            </div>
        </div>
    )
}

export default EditIncident