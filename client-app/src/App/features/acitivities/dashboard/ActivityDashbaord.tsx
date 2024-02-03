import { Activity } from "../../../Models/activity";
import AcivityList from "./ActivityList";
import ActivityDetails from "../ActivityDetails";
import Activityform from "../../form/ActivityForm";
import {Grid } from 'semantic-ui-react';
interface Props {
    activities: Activity[];
    selectedActivity:Activity | undefined;
    selectActivity : (id:string) => void;
    cancelSelectActivity : () => void; 
    editMode: boolean;
    openForm: (id:string)=>void;
    closeForm : () => void;
    createOrEdit : (activity :Activity)=> void;
    deleteActivity : (id:string) => void;
}

export default function ActivityDashboard({activities,selectActivity, selectedActivity, cancelSelectActivity, openForm, closeForm,
     editMode, createOrEdit, deleteActivity }:Props) {
  
    return (
        <Grid>
            <Grid.Column width='10'>
               <AcivityList activities={activities} selectActivity={selectActivity} deleteActivity ={deleteActivity}/>
            </Grid.Column>
            <Grid.Column width={6}>
                {selectedActivity && !editMode &&
                <ActivityDetails 
                activity={selectedActivity} 
                cancelSelectActivity={cancelSelectActivity} 
                openForm = {openForm}
                
                />}
                {editMode &&
                <Activityform closeForm={closeForm} activity={selectedActivity} createOrEdit={createOrEdit}></Activityform>}
            </Grid.Column>
        </Grid>
    )

}