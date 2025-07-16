'use client'
import styles from "./incidents_add.module.css"
import {useState} from "react"
import { useRouter } from "next/navigation";

function AddIncident() {
    const router = useRouter()

    let [incidentName, setIncidentName] = useState("")
    let [incidentDescription, setIncidentDescription] = useState("")
    let [error, setError] = useState("")

    async function add_unres_incident(){
        try{
            let res = await fetch(`${process.env.NEXT_PUBLIC_BASE_URL}/notresolvedincident/`,{
                 headers:{
                "Content-Type":"application/json"
                },
                credentials:"include",
                method:"PUT",
                body:JSON.stringify({
                    name:incidentName,
                    description:incidentDescription
                })
            })

            if(res.ok){
                setError("")
                router.push("/")
            }
            else{
                setError(await res.text())
            }
        }
        catch(error){
            console.log(error)
            setError(error)
        }
    }

    return (
        <div className={styles["add_incident_wrapper"]}>
            <div className = {styles["add_incident_wrapper-header"]}>
                <span className = {styles["special_character"]}>A</span>dd Incident
            </div>
            <form className = {styles["form_wrapper"]}>
                <div className = {styles["error_wrapper"]}>
                    {error}
                </div>
                <input
                  className = {styles["input_name"]} 
                  type="text" 
                  placeholder="Incident Name"
                  onChange={(e)=>{
                    setIncidentName(e.target.value)
                  }}
                />
                <input
                    className = {styles["input_description"]} 
                    type="text" 
                    placeholder="Incident Description"
                    onChange={(e) => {
                        setIncidentDescription(e.target.value)
                    }}
                />
                <button className = {styles["add_button"]}
                    onClick={(e) => {
                        e.preventDefault()
                        add_unres_incident()
                    }}
                >
                    <span className = {styles["special_character"]}>A</span>dd
                </button>
            </form>
        </div>
    )
}

export default AddIncident