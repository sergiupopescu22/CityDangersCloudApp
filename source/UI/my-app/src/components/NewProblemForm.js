

export default function NewProblemForm({location}){
    return (
    <>
        <h3>Report informations</h3>
        <form>
            <label>Pick a location using the map</label>
            <br/>
            <br/>
            <label>
                Selected Latitude:
                <input type="text" name="name" value={location.lat}/>
            </label>
            <br/>
            <label>
                Selected Longitude:
                <input type="text" name="name" value={location.lng}/>
            </label>
            <br/>
            <br/>
            <label>
                Description:
                <input type="text" name="name" />
            </label>
            <br/>
            <button class="button-24-submit" >Submit Issue</button>
        </form>
    </>);
};