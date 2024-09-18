import { Button } from "@nextui-org/react";
import { usePreferences } from "../../hooks/usePreferences";

const DisplaySettings = () => {
  const { changeTheme } = usePreferences();

  return (
    <div className="w-full border p-2 flex flex-col gap-2">
      <h2> DisplaySettings</h2>

      <Button onClick={changeTheme}>Change theme</Button>
    </div>
  );
};

export default DisplaySettings;
