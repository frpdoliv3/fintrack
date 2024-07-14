import { AvatarProps } from "src/types";
import styles from "./Avatar.module.css"
import { Dropdown, OverlayTrigger, Tooltip } from "react-bootstrap";
import { useContext, useState } from "react";
import axios from "axios";
import { UserContext } from "./UserContextProvider";
import { useNavigate } from "react-router";
import { Path } from "@navigation/routes";

function Avatar(props: AvatarProps) {
    const userContext = useContext(UserContext);
    const navigate = useNavigate()
    const avatarPath = props.avatarPath ?? "default_avatar.png"
    const [showDropdown, setShowDropdown] = useState(false)
    const [showOverlay, setShowOverlay] = useState(false)
    const renderTooltip = (tooltipProps: any) => (
        <Tooltip {...tooltipProps}>
            <div className="text-start">
                <p>FinTrack account:</p>
                <p className="text-light">{props.name}</p>
                <p className="text-light">{props.email}</p>
            </div>
        </Tooltip>
    )
    const onToggleOverlay = (desiredValue: boolean) => {
        setShowOverlay(desiredValue)
    }
    const onClickAvatar = () => {
        setShowDropdown(!showDropdown)
        setShowOverlay(false)
    }
    const onLogout = async () => {
        await axios.post("/api/account/logout");
        userContext!.updateUser()
        navigate(Path.Home)
    }
    return (
        <OverlayTrigger 
            overlay={renderTooltip}
            placement="bottom"
            delay={{ show: 300, hide: 300 }}
            show={showDropdown ? false : showOverlay}
            onToggle={onToggleOverlay}
        >
            <Dropdown className="d-inline-block">
                <div id={styles.avatarContainer} onClick={onClickAvatar}>
                    <img id={styles.avatarImage} src={avatarPath} />
                </div>
                <Dropdown.Menu show={showDropdown}>
                    <Dropdown.Item as="button" onClick={onLogout}>Logout</Dropdown.Item>
                </Dropdown.Menu>
            </Dropdown>
        </OverlayTrigger>
    )
}

export default Avatar;