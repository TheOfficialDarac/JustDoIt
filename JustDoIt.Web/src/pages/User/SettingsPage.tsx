import { Button } from "@nextui-org/react";
import { usePreferences } from "../../hooks/usePreferences";

const SettingsPage = () => {
	const { changeTheme } = usePreferences();

	return (
		<>
			ALO
			<div className='m-3 p-3 flex flex-col gap-2'>
				<Button onClick={changeTheme}>Change theme</Button>
			</div>
		</>
	);
};

export default SettingsPage;
