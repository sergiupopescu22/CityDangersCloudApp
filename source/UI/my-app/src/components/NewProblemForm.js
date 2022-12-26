

export default function NewProblemForm({location}){
    return (
    <>
        <h3>Report informations</h3>
        <form>
            <label>
                Location:
                <input type="text" name="name" value={location}/>
            </label>
            <br/>
            <label>
                Description:
                <input type="text" name="name" />
            </label>
            <br/>
            <input type="submit" value="Submit" />
        </form>
    </>);
};