import DisplaySettings from "../../components/settings/DisplaySettings";
import UserSettings from "../../components/settings/UserSettings";

const SettingsPage = ({ authToken, user, fetchUserData }) => {
	return (
		<div className='m-3 p-3 flex flex-col gap-2'>
			<DisplaySettings />
			<UserSettings
				authToken={authToken}
				user={user}
				fetchUserData={fetchUserData}
			/>
		</div>
	);
};

export default SettingsPage;
