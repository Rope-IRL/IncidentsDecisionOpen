'use client'
import { useEffect, useState } from "react"
import Incident from "./components/Incident";
import styles from "./incidents.module.css"
import { BsFeather } from "react-icons/bs";
import { useRouter } from "next/navigation";

function Incidents() {
  const router = useRouter()

  const [incidents, setIncidents] = useState([])
  const [unResolvedState, setUnResolvedState] = useState(true)
  const [resolvedState, setResolvedState] = useState(false)
  const [incidentState, setIncidentState] = useState("notResolved")
  const [error, setError] = useState("")

  const isUnResolvedState = unResolvedState == true ? styles["incidents_type-active"] : styles["incidents_type"]
  const isResolvedState = resolvedState == true ? styles["incidents_type-active"] : styles["incidents_type"]

  async function get_unresolved_incidents_data(){
    let unres_incidents = []
    try{
      const incidents_data = await fetch(`${process.env.NEXT_PUBLIC_BASE_URL}/notresolvedincident`, {
        credentials:"include"
      });
      if(incidents_data.ok == false)
      {
        setError(`${incidents_data.status} ${incidents_data.statusText}`)
      }
      else{
        let res = await incidents_data.json();
        unres_incidents = res
      }
    }
    catch(error){
      console.log(error)
    }
    finally{
      setIncidents(unres_incidents)
    }
  }
  
  async function get_resolved_incidents_data(){
    let res_incidents = []
    try{
      const incidents_data = await fetch(`${process.env.NEXT_PUBLIC_BASE_URL}/resolvedincident`, {
        credentials:"include"
      });
      if(incidents_data.ok == false)
      {
        setError(`${incidents_data.status} ${incidents_data.statusText}`)
      }
      else{
        let res = await incidents_data.json();
        res_incidents = res
      }
    }
    catch(error){
      console.log(error)
    }
    finally{
      setIncidents(res_incidents)
    }
  }

  useEffect(() =>{
    get_unresolved_incidents_data()
  }, []);

  const changeCurrentResolvedState = (state) =>{
    switch (state) {
      case "notResolved":
        setUnResolvedState(true) 
        setResolvedState(false)
        get_unresolved_incidents_data();
        break;
      case "resolved":
        setResolvedState(true);
        setUnResolvedState(false);
        get_resolved_incidents_data();
        break;
      default:
        break;
    }
  }

  return (
    <div className = {styles["incidents_wrapper"]}>
      <div className = {styles["incidents_wrapper-header"]}>Incidents</div>
      <div className = {styles["incidents_wrapper-incident_types"]}>
        <button className = {isUnResolvedState} 
          onClick={() =>{
            changeCurrentResolvedState("notResolved")
            setIncidentState("notResolved")
          }}
        >
          Not <span className = {styles["special_character"]}>R</span>esolved
        </button>

        <button className = {isResolvedState} 
          onClick = {() => {
            changeCurrentResolvedState("resolved")
            setIncidentState("resolved")
          }}
        >
          Resolved
        </button>
      </div>
      <div className = {styles["add_btn_wrapper"]}>
      <button className = {styles["add_btn_container"]}
        onClick={() =>{
          router.push("/incidents/add")
        }}
      >
        <div>
          <span className = {styles["special_character"]}>A</span>dd Incident
        </div> 
        <BsFeather />
      </button>
      </div>
      <div className = {styles["incidents_container"]}>
        <div className = {styles["error_wrapper"]}>
          {error}
        </div>
        <div className = {styles["incidents_container-content"]}>
          {
            incidents == [] ? "Loading..." : incidents.map(e =>{
              return <Incident key = {e.id} id = {e.id} name = {e.name} description = 
                {e.description} createdAt = {e.createdAt} type = {incidentState}/>
            })
          }
        </div>
      </div>
    </div>
  )
}
export default Incidents